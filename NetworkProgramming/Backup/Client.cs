using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;


// C#网络编程 - Part.4

// 此段代码演示由客户端发送文件到服务端
// 与此对应的代码为ServerConsole项目中的Server1.cs

namespace ClientConsole {

	class Program {
		static void Main(string[] args) {

			ServerClient client = new ServerClient();
			string input;
			string path = Environment.CurrentDirectory + "/";

			do {
				Console.WriteLine("Send File:    S1 - Client01.jpg, S2 - Client02.jpg, S3 - Client03.jpg");
				Console.WriteLine("Receive File: R1 - Server01.jpg, R1 - Server02.jpg, R3- Server03.jpg");
				Console.WriteLine("Press 'Q' to exit. \n");
				Console.Write("Enter your choice: ");
				input = Console.ReadLine();
				switch(input.ToUpper()){
					case "S1":
						client.BeginSendFile(path + "Client01.jpg");
						break;
					case "S2":
						client.BeginSendFile(path + "Client02.jpg");
						break;
					case "S3":
						client.BeginSendFile(path + "Client02.jpg");
						break;
					case "R1":
						client.BeginReceiveFile("Server01.jpg");
						break;
					case "R2":
						client.BeginReceiveFile("Server01.jpg");
						break;
					case "R3":
						client.BeginReceiveFile("Server01.jpg");
						break;
				}				
			} while (input.ToUpper() != "Q");

			client.Dispose();
		}
	}


	public class ServerClient :IDisposable {
		private const int BufferSize = 8192;
		private byte[] buffer;
		private TcpClient client;
		private NetworkStream streamToServer;

		public ServerClient() {
			try {
				client = new TcpClient();
				client.Connect("localhost", 8500);		// 与服务器连接
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				return;
			}
			buffer = new byte[BufferSize];

			// 打印连接到的服务端信息
			Console.WriteLine("Server Connected！{0} --> {1}\n",
				client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

			streamToServer = client.GetStream();
		}

		// 发送消息到服务端
		public void SendMessage(string msg) {

			byte[] temp = Encoding.Unicode.GetBytes(msg);	// 获得缓存
			try {
				lock (streamToServer) {
					streamToServer.Write(temp, 0, temp.Length);	// 发往服务器
				}
				Console.WriteLine("Sent: {0}", msg);
			} catch (Exception ex) {
				Console.WriteLine("+++++");
				Console.WriteLine(ex.Message);
				return;
			}
		}

		// 发送文件 - 异步方法
		public void BeginSendFile(string filePath) {
			ParameterizedThreadStart start =
				new ParameterizedThreadStart(SendFile);
			start.BeginInvoke(filePath, null, null);
		}

		private void SendFile(object obj) {
			string filePath = obj as string;
			SendFile(filePath);
		}
		
		// 发送文件 -- 同步方法
		public void SendFile(string filePath) {

			IPAddress ip = IPAddress.Parse("127.0.0.1");
			TcpListener listener = new TcpListener(ip, 0);
			listener.Start();

			// 获取本地侦听的端口号
			IPEndPoint endPoint = listener.LocalEndpoint as IPEndPoint;
			int listeningPort = endPoint.Port;

			// 获取发送的协议字符串
			string fileName = Path.GetFileName(filePath);
			FileProtocol protocol =
				new FileProtocol(FileRequestMode.Send, listeningPort, fileName);
			string pro = protocol.ToString();

			SendMessage(pro);		// 发送协议到服务端

			// 中断，等待远程连接
			TcpClient localClient = listener.AcceptTcpClient();
			Console.WriteLine("Start sending file...");
			NetworkStream stream = localClient.GetStream();

			// 创建文件流
			FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);			
			byte[] fileBuffer = new byte[1024];		// 每次传1KB
			int bytesRead;
			int totalBytes = 0;

			// 创建获取文件发送状态的类
			SendStatus status = new SendStatus(filePath);

			// 将文件流转写入网络流
			try {
				do {
					Thread.Sleep(10);			// 为了更好的视觉效果，暂停10毫秒:-)
					bytesRead = fs.Read(fileBuffer, 0, fileBuffer.Length);					
					stream.Write(fileBuffer, 0, bytesRead);
					totalBytes += bytesRead;			// 发送了的字节数
					status.PrintStatus(totalBytes);	// 打印发送状态
				} while (bytesRead > 0);
				Console.WriteLine("Total {0} bytes sent, Done!", totalBytes);
			} catch {
				Console.WriteLine("Server has lost...");
			}
			
			stream.Dispose();
			fs.Dispose();
			localClient.Close();
			listener.Stop();
		}
		
		// 接收文件 -- 异步方法
		public void BeginReceiveFile(string fileName) {
			ParameterizedThreadStart start =
				new ParameterizedThreadStart(ReceiveFile);
			start.BeginInvoke(fileName, null, null);
		}

		public void ReceiveFile(object obj) {
			string fileName = obj as string;
			ReceiveFile(fileName);
		}


		// 接收文件 -- 同步方法
		public void ReceiveFile(string fileName) {

			IPAddress ip = IPAddress.Parse("127.0.0.1");
			TcpListener listener = new TcpListener(ip, 0);
			listener.Start();

			// 获取本地侦听的端口号
			IPEndPoint endPoint = listener.LocalEndpoint as IPEndPoint;
			int listeningPort = endPoint.Port;

			// 获取发送的协议字符串
			FileProtocol protocol =
				new FileProtocol(FileRequestMode.Receive, listeningPort, fileName);
			string pro = protocol.ToString();

			SendMessage(pro);		// 发送协议到服务端

			// 中断，等待远程连接
			TcpClient localClient = listener.AcceptTcpClient();
			Console.WriteLine("Start sending file...");
			NetworkStream stream = localClient.GetStream();

			// 获取文件保存的路劲
			string filePath =
				Environment.CurrentDirectory + "/" + generateFileName(fileName);

			// 创建文件流
			FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
			byte[] fileBuffer = new byte[1024];		// 每次传1KB
			int bytesRead;
			int totalBytes = 0;

			// 从缓存buffer中读入到文件流中
			do {
				bytesRead = stream.Read(buffer, 0, BufferSize);
				fs.Write(buffer, 0, bytesRead);
				totalBytes += bytesRead;
				Console.WriteLine("Receiving {0} bytes ...", totalBytes);
			} while (bytesRead > 0);

			Console.WriteLine("Total {0} bytes received, Done!", totalBytes);

			fs.Dispose();			
			stream.Dispose();
			localClient.Close();
			listener.Stop();
		}


		// 随机获取一个图片名称
		private string generateFileName(string fileName) {
			DateTime now = DateTime.Now;
			return String.Format(
				"{0}_{1}_{2}_{3}", now.Minute, now.Second, now.Millisecond, fileName
			);
		}

		public void Dispose() {
			if (streamToServer != null)
				streamToServer.Dispose();
			if (client != null)
				client.Close();
		}

	}

}