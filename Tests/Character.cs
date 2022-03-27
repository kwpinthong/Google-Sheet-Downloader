namespace com.kwpinthong.GoogleSheetDownloader.Test
{
    public enum Gender
    {
        Male,
        Female,
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
}
