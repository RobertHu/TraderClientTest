using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

// C#网络编程 - Part.4

// 此段代码演示由客户端发送文件到服务端
// 与此对应的代码为ClientConsole项目中的Client1.cs

namespace ServerConsole {
	class Server {
		static void Main(string[] args) {
			Console.WriteLine("Server is running ... ");
			IPAddress ip = IPAddress.Parse("127.0.0.1");
			TcpListener listener = new TcpListener(ip, 8500);

			listener.Start();			// 开启对控制端口 8500 的侦听
			Console.WriteLine("Start Listening ...");

			while (true) {
				// 获取一个连接，同步方法，在此处中断
				TcpClient client = listener.AcceptTcpClient();				
				RemoteClient wapper = new RemoteClient(client);
				wapper.BeginRead();
			}
		}
	}

	public class RemoteClient {
		private TcpClient client;
		private NetworkStream streamToClient;
		private const int BufferSize = 8192;
		private byte[] buffer;
		private ProtocolHandler handler;
		
		public RemoteClient(TcpClient client) {
			this.client = client;

			// 打印连接到的客户端信息
			Console.WriteLine("\nClient Connected！{0} <-- {1}",
				client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

			// 获得流
			streamToClient = client.GetStream();
			buffer = new byte[BufferSize];

			handler = new ProtocolHandler();
		}

		// 开始进行读取
		public void BeginRead() {		
			AsyncCallback callBack = new AsyncCallback(OnReadComplete);
			streamToClient.BeginRead(buffer, 0, BufferSize, callBack, null);
		}

		// 再读取完成时进行回调
		private void OnReadComplete(IAsyncResult ar) {
			int bytesRead = 0;
			try {
				lock (streamToClient) {
					bytesRead = streamToClient.EndRead(ar);
					Console.WriteLine("Reading data, {0} bytes ...", bytesRead);
				}
				if (bytesRead == 0) throw new Exception("读取到0字节");

				string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
				Array.Clear(buffer,0,buffer.Length);		// 清空缓存，避免脏读

				// 获取protocol数组
				string[] protocolArray = handler.GetProtocol(msg);
				foreach (string pro in protocolArray) {
					// 这里异步调用，不然这里可能会比较耗时
					ParameterizedThreadStart start =
						new ParameterizedThreadStart(handleProtocol);
					start.BeginInvoke(pro, null, null);					
				}

				// 再次调用BeginRead()，完成时调用自身，形成无限循环
				lock (streamToClient) {
					AsyncCallback callBack = new AsyncCallback(OnReadComplete);
					streamToClient.BeginRead(buffer, 0, BufferSize, callBack, null);
				}
			} catch(Exception ex) {
				if(streamToClient!=null)
					streamToClient.Dispose();
				client.Close();
				Console.WriteLine(ex.Message);		// 捕获异常时退出程序
			}
		}

		// 处理protocol
		private void handleProtocol(object obj) {
			string pro = obj as string;
			ProtocolHelper helper = new ProtocolHelper(pro);
			FileProtocol protocol = helper.GetProtocol();

			if (protocol.Mode == FileRequestMode.Send) {
				// 客户端发送文件，对服务端来说则是接收文件
				receiveFile(protocol);
			} else if (protocol.Mode == FileRequestMode.Receive) {
				// 客户端接收文件，对服务端来说则是发送文件
				sendFile(protocol);
			}
		}

		// 发送文件
		private void sendFile(FileProtocol protocol) {
			TcpClient localClient;
			NetworkStream streamToClient = getStreamToClient(protocol, out localClient);

			// 获得文件的路径
			string filePath = Environment.CurrentDirectory + "/" + protocol.FileName;

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
					streamToClient.Write(fileBuffer, 0, bytesRead);
					totalBytes += bytesRead;			// 发送了的字节数
					status.PrintStatus(totalBytes);	// 打印发送状态
				} while (bytesRead > 0);
				Console.WriteLine("Total {0} bytes sent, Done!", totalBytes);
			} catch {
				Console.WriteLine("Server has lost...");
			}

			streamToClient.Dispose();
			fs.Dispose();
			localClient.Close();
		}
		

		// 接收文件
		private void receiveFile(FileProtocol protocol) {
			TcpClient localClient;
			NetworkStream streamToClient = getStreamToClient(protocol, out localClient);
			if (streamToClient == null) return;

			// 随机生成一个在当前目录下的文件名称
			string path =
				Environment.CurrentDirectory + "/" + generateFileName(protocol.FileName);

			byte[] fileBuffer = new byte[1024];	// 每次收1KB
			FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);

			// 从缓存buffer中读入到文件流中
			int bytesRead;
			int totalBytes = 0;
			do {
				bytesRead = streamToClient.Read(buffer, 0, BufferSize);				
				fs.Write(buffer, 0, bytesRead);
				totalBytes += bytesRead;
				Console.WriteLine("Receiving {0} bytes ...", totalBytes);
			} while (bytesRead > 0);

			Console.WriteLine("Total {0} bytes received, Done!", totalBytes);

			streamToClient.Dispose();
			fs.Dispose();
			localClient.Close();
		}

		// 获取连接到远程的流 -- 公共方法
		private NetworkStream getStreamToClient(FileProtocol protocol, out TcpClient localClient) {
			// 获取远程客户端的位置
			IPEndPoint endpoint = client.Client.RemoteEndPoint as IPEndPoint;
			IPAddress ip = endpoint.Address;

			// 使用新端口号，获得远程用于接收文件的端口
			endpoint = new IPEndPoint(ip, protocol.Port);

			// 连接到远程客户端
			try {
				localClient = new TcpClient();
				localClient.Connect(endpoint);
			} catch {
				Console.WriteLine("无法连接到客户端 --> {0}", endpoint);
				localClient = null;
				return null;
			}

			// 获取发送文件的流
			NetworkStream streamToClient = localClient.GetStream();
			return streamToClient;
		}


		// 随机获取一个图片名称
		private string generateFileName(string fileName) {
			DateTime now = DateTime.Now;
			return String.Format(
				"{0}_{1}_{2}_{3}", now.Minute, now.Second, now.Millisecond, fileName
			);
		}
	}
}
