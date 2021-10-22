using System;
using System.Collections.Generic;

namespace discordAIO
{
	public class RandomInfo
	{
		public RandomInfo(RandomCharacters randomCharacters)
		{
			this.randomCharacters_0 = randomCharacters;
		}

		public FileInfo getRandomFileInfo()
		{
			/*if (this.random_0.Next(0, 7) == 0)
			{
				return new FileInfo
				{
					Title = this.method_0(2),
					Description = this.method_0(2),
					Product = this.method_0(2),
					Company = this.method_0(2),
					Copyright = this.method_0(2),
					Trademark = this.method_0(2),
					MajorVersion = this.random_0.Next(0, 10).ToString(),
					MinorVersion = this.random_0.Next(0, 10).ToString(),
					BuildPart = this.random_0.Next(0, 10).ToString(),
					PrivatePart = this.random_0.Next(0, 10).ToString()
				};
			}*/
			int num = this.int_0;
			do
			{
				num = this.random_0.Next(0, this.list_0.Count - 1);
			}
			while (num == this.int_0);
			this.int_0 = num;
			return this.list_0[num];
		}
		/*
				private string method_0(int int_1)
				{
					List<string> list = new List<string>();
					for (int i = 0; i < int_1; i++)
					{
						list.Add(this.randomCharacters_0.getRandomCharacters(this.random_0.Next(5, 10)));
					}
					return string.Join(" ", list);
				}
				*/
		private readonly Random random_0 = new Random();
		private readonly RandomCharacters randomCharacters_0;
		private int int_0;

		private readonly List<FileInfo> list_0 = new List<FileInfo>
		{
			new FileInfo
			{
				Title = "chrome_exe",
				Description = "Google Chrome",
				Product = "Google Chrome",
				Company = "Google LLC",
				Copyright = "Copyright 2020 Google LLC. All rights reserved.",
				Trademark = "",
				MajorVersion = "89",
				MinorVersion = "0",
				BuildPart = "4389",
				PrivatePart = "90"
			},
			new FileInfo
			{
				Title = "vlc",
				Description = "VLC media player",
				Product = "VLC media player",
				Company = "VideoLAN",
				Copyright = "Copyright © 1996-2018 VideoLAN and VLC Author",
				Trademark = "VLC media player, VideoLAN and x264 are registered trademarks from VideoLAN",
				MajorVersion = "3",
				MinorVersion = "0",
				BuildPart = "3",
				PrivatePart = "0"
			},
			new FileInfo
			{
				Title = "HWMonitor.exe",
				Description = "HWMonitor",
				Product = "CPUID Hardware Monitor",
				Company = "CPUID",
				Copyright = "(c)2008-2018 CPUID.  All rights reserved.",
				Trademark = "",
				MajorVersion = "1",
				MinorVersion = "3",
				BuildPart = "4",
				PrivatePart = "0"
			},
			new FileInfo
			{
				Title = "Rockstar-Games-Launcher.exe",
				Description = "Rockstar Games Launcher",
				Product = "Rockstar Games Launcher",
				Company = "Rockstar Games.",
				Copyright = "Rockstar Games Inc. (C) 2005-2019 Take Two Interactive. All rights reserved",
				Trademark = "Rockstar Games Inc. (C) 2005-2019 Take Two Interactive. All rights reserved",
				MajorVersion = "1",
				MinorVersion = "0",
				BuildPart = "35",
				PrivatePart = "340"
			},
			new FileInfo
			{
				Title = "Photoshop",
				Description = "Adobe Photoshop 2020",
				Product = "Adobe Photoshop 2020",
				Company = "Adobe",
				Copyright = "© 1990-2019 Adobe. All rights reserved.",
				Trademark = "",
				MajorVersion = "21",
				MinorVersion = "0",
				BuildPart = "0",
				PrivatePart = "37"
			},
			new FileInfo
			{
				Title = "Spotify",
				Description = "Spotify",
				Product = "Spotify",
				Company = "Spotify Ltd",
				Copyright = "Copyright (c) 2021, Spotify Ltd",
				Trademark = "",
				MajorVersion = "1",
				MinorVersion = "1",
				BuildPart = "54",
				PrivatePart = "592"
			},
			new FileInfo
			{
				Title = "CamtasiaStudio.exe",
				Description = "TechSmith Camtasia 2018",
				Product = "Camtasia",
				Company = "TechSmith Corporation",
				Copyright = "Copyright © 2011-2018 TechSmith Corporation. All rights reserved.",
				Trademark = "18",
				MajorVersion = "0",
				MinorVersion = "0",
				BuildPart = "31",
				PrivatePart = "0"
			}
			// New info
		};
	}
}