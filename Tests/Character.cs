using System;
using UnityEngine;

namespace com.kwpinthong.GoogleSheetDownloader.Example
{
    public enum Gender
    {
        Male,
        Female,
    }

    [Serializable]
    public class Character
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private Gender gender;

        public int Id { get => id; private set => id = value; }
        public string Name { get => name; private set => name = value; }
        public Gender Gender { get => gender; private set => gender = value; }
    }
}
