using System.Collections.Generic;
using UnityEngine;

namespace com.kwpinthong.GoogleSheetDownloader
{
    public abstract class RemoteDatabase<T> : ScriptableObject
    {
        public string GoogleSheetId;
        public List<GoogleSheetDocument> Documents = new List<GoogleSheetDocument>();

        protected List<T> DownloadGoogleSheet(string GID)
        {
            return GoogleSheet.CSVDownload<T>(GoogleSheetId, GID);
        }
        
        public abstract void CreateDatabase();
    }
}
