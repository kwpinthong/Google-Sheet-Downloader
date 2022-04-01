using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using kwpinthong.GoogleSheetDownloader.Data;

namespace kwpinthong.GoogleSheetDownloader
{
    public abstract class RemoteDatabase<T> : ScriptableObject
    {
#if UNITY_EDITOR
        public string GoogleSheetId;
        public List<GoogleSheetDocument> Documents = new List<GoogleSheetDocument>();
#endif

        [Header("Database")]
        [PropertyOrder(1)]
        [SerializeField]
        protected List<T> list = new List<T>();
        public List<T> Database => list;

#if UNITY_EDITOR
        [Button, PropertyOrder(0)]
        public virtual void CreateDatabase()
        {
            if (string.IsNullOrEmpty(GoogleSheetId))
            {
                throw new System.Exception("GoogleSheetId must not null, stop download");
            }
            list.Clear();
            foreach (var document in Documents)
            {
                if (string.IsNullOrEmpty(document.GID))
                {
                    throw new System.Exception("document.GID must not null, stop download");
                }
                var remoteList = DownloadGoogleSheet(document.GID);
                list.AddRange(remoteList);
            }
            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
            Debug.Log($"Completed Remote {GetType()} database, save asset");
        }

        protected List<T> DownloadGoogleSheet(string GID)
        {
            return GoogleSheet.CSVDownload<T>(GoogleSheetId, GID);
        }
#endif
    }
}
