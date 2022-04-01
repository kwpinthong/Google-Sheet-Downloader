using GoogleSheetDownloader.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GoogleSheetDownloader
{
    public abstract class Database<T> : ScriptableObject
    {
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
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
#endif
            Debug.Log($"Completed Remote {GetType()} database, save asset");
        }

        protected List<T> DownloadGoogleSheet(string GID)
        {
            return GoogleSheet.CSVDownload<T>(GoogleSheetId, GID);
        }

        [Header("Database")]
        [PropertyOrder(1)]
        [SerializeField] protected List<T> database;

        public int Count => database == null ? 0 : database.Count;

        /// <summary>
        /// clone current database to newlist for other class read/edit
        /// </summary>
        public List<T> CloneDatabase
        {
            get
            {
                if (database == null)
                    return null;
                List<T> cloneDatabase = new List<T>();
                cloneDatabase.AddRange(database);
                return cloneDatabase;
            }
        }

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
                if (match(database[i])) return database[i];
            }
            return default;
        }

        public List<T> FindAll(Predicate<T> match)
        {
            if (database == null)
                return null;
            List<T> allList = new List<T>();
            for (int i = 0; i < database.Count; i++)
            {
                if (match(database[i])) allList.Add(database[i]);
            }
            return allList;
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
