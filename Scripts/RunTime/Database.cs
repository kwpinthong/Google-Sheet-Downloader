using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
#if UNITY_EDITOR
using GoogleSheetDownloader.Data;
using UnityEditor;
#endif

namespace GoogleSheetDownloader
{
    public abstract class Database<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] protected string GoogleSheetId;
        [SerializeField] protected List<GoogleSheetDocument> Documents;

        [Button, PropertyOrder(0)]
        protected virtual void CreateDatabase()
        {
            if (string.IsNullOrEmpty(GoogleSheetId))
            {
                throw new System.Exception("GoogleSheetId must not null, stop download");
            }
            if (database == null)
                database = new List<T>();
            database.Clear();
            foreach (var document in Documents)
            {
                if (string.IsNullOrEmpty(document.GID))
                {
                    throw new System.Exception("document.GID must not null, stop download");
                }
                var remoteList = DownloadGoogleSheet(document.GID);
                database.AddRange(remoteList);
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

        [Header("Database")]
        [PropertyOrder(1)]
        [SerializeField] protected List<T> database;

        public int Count => database == null ? 0 : database.Count;

        public void AddRange(List<T> itemList)
        {
            if (database == null)
                database = new List<T>();
            database.AddRange(itemList);
        }

        public bool Clear()
        {
            if (database == null)
                return false;
            database.Clear();
            return true;
        }

        public bool Contains(T item)
        {
            if (database == null)
                return false;
            return database.Contains(item);
        }

        public T Find(Predicate<T> match)
        {
            if (database == null)
                return default;
            for (int i = 0; i < database.Count; i++)
            {
                var item = database[i];
                if (match(item)) return item;
            }
            return default;
        }

        public T Get(int index)
        {
            if (database == null)
                return default;
            if (index >= database.Count)
                return default;
            return database[index];
        }
    }
}
