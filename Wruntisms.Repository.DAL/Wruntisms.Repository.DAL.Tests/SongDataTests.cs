namespace Wruntisms.Repository.DAL.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SongDataTests
    {
        private WruntEntity entity = new WruntEntity();

        private const string SongName = "This is only a test";
        private readonly int internalId = int.MaxValue / 2;
        private readonly string externalKey = Guid.NewGuid().ToString();
        
        [TestMethod]
        public void InitializeTest()
        {
            var locId = internalId;
            var song = new SongData
            {
                SongName = SongName,
                SongId = locId,
                SongKey = externalKey
            };

            Assert.AreEqual(song.SongName, SongName);
            Assert.AreEqual(song.SongId, locId);
            Assert.AreEqual(song.SongKey, externalKey);
        }

        [TestMethod]
        public void InitializeDataRecordTest()
        {
            var locId = internalId;
            var song = new SongData
            {
                SongName = SongName,
                SongId = locId,
                SongKey = externalKey
            };

            song.InitializeDataRecord();
            var rec = song.SongRecord;

            Assert.Equals(song.SongName, rec.SongName);
            Assert.Equals(song.SongId, rec.SongId);
            Assert.Equals(song.SongKey, rec.SongKey);
        }

        [TestMethod]
        public void JsonTest()
        {
            var locId = internalId + 2;
            var song = new SongData
            {
                SongName = SongName,
                SongId = locId,
                SongKey = externalKey
            };

            var jsonData = song.ToJsonString();

            var copyObject = SongData.ToSong(jsonData);

            Assert.Equals(song, copyObject);
        }

        [TestMethod]
        public void CreateAndDeleteTest()
        {
            var locId = internalId + 3;
            var song = new SongData
            {
                SongName = SongName,
                SongId = locId,
                SongKey = externalKey
            };

            song.
        }
    }
}
