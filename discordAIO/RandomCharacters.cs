using System;
using System.Text;

namespace discordAIO
{
	public class RandomCharacters
	{
		public string getRandomCharacters(int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i <= length; i++)
			{
				int index = this._random.Next(0, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Length);
				stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[index]);
			}
			return stringBuilder.ToString();
		}
		private readonly Random _random = new Random();
		private const string string_0 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
	}
}