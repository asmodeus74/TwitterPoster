using System;
using System.IO;

namespace TwitterPoster
{
	public class AppSettings
	{
		public string accessToken { get; set; }
		public string accessTokenSecret { get; set; }
		public string apiKey { get; set; }
		public string apiSecret { get; set; }

		private static AppSettings instance;
		public static string ConfigFile { get; set; }


		public static AppSettings Instance
		{
			get {
				if (instance == null) {
					instance = Load ();
				}
				return instance;
			}
		 }

		static AppSettings ()
		{
		}

		private static AppSettings Load() 
		{
			ConfigFile = "TwitterPoster.config";
			var file = ConfigFile;
			var json = File.ReadAllText (file);
			return JsonHelper.FromJson<AppSettings> (json);
		}
	}
}

