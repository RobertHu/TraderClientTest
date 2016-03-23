using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;

namespace ServerConsole {

	public class ProtocolHandler {

		private string partialProtocal;	// 保存不完整的协议
		
		public ProtocolHandler() {
			partialProtocal = "";		
		}

		public string[] GetProtocol(string input) {
			return GetProtocol(input, null);
		}
		
		// 获得协议
		private string[] GetProtocol(string input, List<string> outputList) {			
			
			if (outputList == null)
				outputList = new List<string>();

			if (String.IsNullOrEmpty(input))
				return outputList.ToArray();

			if (!String.IsNullOrEmpty(partialProtocal))
				input = partialProtocal + input;

			string pattern = "(^<protocol>.*?</protocol>)";

			// 如果有匹配，说明已经找到了，是完整的协议
			if (Regex.IsMatch(input, pattern)) {

				// 获取匹配的值
				string match = Regex.Match(input, pattern).Groups[0].Value;
				outputList.Add(match);
				partialProtocal = "";

				// 缩短input的长度
				input = input.Substring(match.Length);

				// 递归调用
				GetProtocol(input, outputList);

			} else {
				// 如果不匹配，说明协议的长度不够，
				// 那么先缓存，然后等待下一次请求
				partialProtocal = input;
			}

			return outputList.ToArray();
		}
		
		// 用于测试的程序
		public static void Test() {
			ProtocolHandler handler = new ProtocolHandler();

			// 一次完整的协议
			string protocol = "<protocol><file name=\"client01.jpg\" mode=\"send\" ip=\"127.0.0.1\" port=\"8005\" /></protocol>";
			string input = protocol;
			PrintOutput(handler, input);

			// 两条协议合并到了一起
			input = input + input;
			PrintOutput(handler, input);

			// 两条协议拆开到达
			input = "<protocol><file name=\"client01.jpg\" mod";
			PrintOutput(handler, input);

			input = "e=\"send\" ip=\"127.0.0.1\" port=\"8005\" /></protocol>";
			PrintOutput(handler, input);
		}

		private static void PrintOutput(ProtocolHandler handler, string input) {
			string[] protocols = handler.GetProtocol(input);
			foreach (string pro in protocols) {
				Console.WriteLine(pro);
			}
		}
	}
}
