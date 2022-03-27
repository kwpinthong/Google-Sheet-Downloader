using com.kwpinthong.GoogleSheetDownloader;
using com.kwpinthong.GoogleSheetDownloader.Example;
using NUnit.Framework;

namespace Tests
{
    public class GoogleSheetDownloaderTests
    {
        [Test]
        public void CSVDownload()
        {
            var result = GoogleSheet.CSVDownload<Character>("1YXHTQOzvr0l0VM4EuLeb1C9rVzW3-CZxTWjWpk-riy4", "1778936164");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Sam", result[0].Name);
            Assert.AreEqual("Rose", result[1].Name);
            Assert.AreEqual(Gender.Male, result[0].Gender);
            Assert.AreEqual(Gender.Female, result[1].Gender);
        }
    }
}
