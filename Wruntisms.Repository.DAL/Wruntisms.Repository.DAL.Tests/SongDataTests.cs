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
            var locId = internalId + 1;
            var song = new SongData
            {
                SongName = SongName,
                SongId = locId,
                SongKey = externalKey
            };

            var rec = song.InitializeDataRecord();

            Assert.AreEqual(song.SongName, rec.SongName);
            Assert.AreEqual(song.SongId, rec.SongId);
            Assert.AreEqual(song.SongKey, rec.SongKey);
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

            Assert.AreEqual(song.SongName, copyObject.SongName);
            Assert.AreEqual(song.SongId, copyObject.SongId);
            Assert.AreEqual(song.SongKey, copyObject.SongKey);
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

            Assert.IsTrue(song.CreateDataRecord());

            Assert.IsTrue(song.VerifyDataRecord(song.SongRecord));

            Assert.IsTrue(song.DeleteDataRecord());

            Assert.IsFalse(song.VerifyDataRecord(song.SongRecord));
        }
    }
}
