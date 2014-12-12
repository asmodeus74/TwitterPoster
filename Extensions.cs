using System;

namespace TwitterPoster
{
	public static class Extensions
	{
		public static long ToUnixTimestamp(this DateTime date)
		{
			DateTime origin = new DateTime (1970, 1, 1, 0, 0, 0, 0);
			TimeSpan diff = date - origin;
			return (long )Math.Floor(diff.TotalSeconds);
		}
	}
}

