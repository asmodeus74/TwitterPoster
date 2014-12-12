using System;
using System.IO;
using TweetSharp;
using System.Collections.Generic;
using System.Text;

namespace TwitterPoster
{
	class MainClass
	{
		public static void RandomOrder(StreamReader sr, StreamWriter sw)
		{
			var list = new List<string> ();

			while (true) {
				var line = sr.ReadLine ();
				if (line == null) {
					break;
				}
				list.Add (line);
			}

			var rand = new Random ((int)DateTime.Now.Ticks);
			while (true) {
				int index = rand.Next (list.Count);
				string s = list [index];
				sw.WriteLine (s);
				list.RemoveAt (index);

				if (list.Count == 0) {
					break;
				}
			}
		}

		public static int GetLineNumber(string fileName)
		{
			try {
				var buffer = File.ReadAllText (fileName + ".pos");
				int index = int.Parse (buffer);
				return index;
			} catch {
				return 0;
			}
		}

		public static void SetLineNumber(int index, string fileName)
		{
			File.WriteAllText (fileName + ".pos", index.ToString ());
		}

		public static void QuitWithMessage(string msg)
		{
			Console.WriteLine (msg);
			Console.WriteLine ("\r\nUsage: TwitterPoster.exe -i <Input File> -t <Tags>");
			Environment.Exit (0);
		}

		public static void Main (string[] args)
		{
			string inFile = null, tags = null;

			for (int i = 0; i < args.Length; i++) {
				string s = args [i];
				switch (s) {
				case "-i":
					if (++i >= args.Length) {
						QuitWithMessage ("This parameter requires a filename");
					}
					inFile = args [i];
					break;

				case "-t":
					if (++i >= args.Length) {
						QuitWithMessage ("This parameter requires a series of tags in double quotes");
					}
					tags = args [i];
					break;
				}
			}
				
			if (inFile == null) {
				QuitWithMessage ("Input filename required");
			}
				
			var service = new TwitterService(AppSettings.Instance.apiKey, AppSettings.Instance.apiSecret);
			service.AuthenticateWith(AppSettings.Instance.accessToken, AppSettings.Instance.accessTokenSecret);

			var lineNumber = GetLineNumber (inFile);
			var index = lineNumber;
			StreamReader reader = null;
			try {
				reader = File.OpenText (inFile);
			}
			catch {
				QuitWithMessage ("Error opening input file " + inFile);
			}

			while (lineNumber-- > 0) {
				reader.ReadLine ();
			}

			var line = reader.ReadLine ();
			var dict = line.Split (':');
			if (dict.Length != 2) {
				return;
			}

			var es = dict [0].Trim ();
			var en = dict [1].Trim ();

			var sb = new StringBuilder();
			sb.Append(string.Format ("{0}\r\n{1}", es, en));
			if (!string.IsNullOrEmpty(tags)) {
				sb.Append("\r\n").Append(tags);
			}
			string status = sb.ToString();

			var tweet = new SendTweetOptions { Status = status };
			try {
				var st = service.SendTweet (tweet);
				if (st == null) {
					QuitWithMessage("Sending tweet failed");
				}

				Console.WriteLine("Tweet successfully sent");
			}
			catch {
				QuitWithMessage("Sending tweet failed");
			}

			SetLineNumber (index + 1, inFile);
		}
	}
}

