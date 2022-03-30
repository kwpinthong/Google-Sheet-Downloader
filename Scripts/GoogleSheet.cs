using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using UnityEngine;

namespace kwpinthong.GoogleSheetDownloader
{
    public static class GoogleSheet
    {
        private const string csvDataPath = "/CSVData/{0}.csv";

        private static string FormatGoogleSheetLink(string sheetID, string gid, string format)
        {
            return $"https://docs.google.com/spreadsheets/d/{sheetID}/export?format={format}&gid={gid}";
        }

        private static List<T> DownloadEntry<T>(string url, string fileName)
        {
            List<T> list = new List<T>();
            var filePath = Application.dataPath + string.Format(csvDataPath, fileName);
            using (var client = new WebClient())
            {
                client.DownloadFile(url, filePath);
            }
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IncludePrivateMembers = true,
                DetectColumnCountChanges = true,
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<T>();
                    list.Add(record);
                }
            }
            return list;
        }

        public static List<T> CSVDownload<T>(string sheetID, string gid)
        {
            var url = FormatGoogleSheetLink(sheetID, gid, "csv");
            return DownloadEntry<T>(url, $"{sheetID}_{gid}");
        }
    }
}
