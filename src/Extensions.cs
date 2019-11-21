using System;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Configuration;
using SubtitlesParser.Classes;

namespace SubtitlesTranslator
{
    public static class Extensions
    {
        private const string TimeFormat = "hh\\:mm\\:ss\\,fff";

        public static string Header(this SubtitleItem s)
        {
            var startTime = new TimeSpan(0, 0, 0, 0, s.StartTime);
            var endTime = new TimeSpan(0, 0, 0, 0, s.EndTime);
            return
                $"{startTime.ToString(TimeFormat, CultureInfo.InvariantCulture)} --> {endTime.ToString(TimeFormat, CultureInfo.InvariantCulture)}";
        }
    }
}