﻿namespace Wruntisms.Repository.DAL
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    [DataContract(Name = "song")]
    public class SongData : IDataStore<Song>, IJsonSerializable<SongData>
    {
        [DataMember(Name = "name")]
        public string SongName { get; set; }
        [DataMember(Name = "id")]
        public int SongId { get; set; }
        [DataMember(Name = "externalId")]
        public string SongKey { get; set; }

        public Song SongRecord { get; private set; }

        public SongData()
        {
            Initialize();
        }

        public SongData(string jsonString)
        {
            var obj = ToObject(jsonString);
            SongName = obj.SongName;
            SongId = obj.SongId;
            SongKey = obj.SongKey;
            Initialize();

            SongRecord = GetDataRecordInternalKey();
        }

        public SongData(int internalId)
        {
            SongId = internalId;

            Initialize();

            SongRecord = GetDataRecordInternalKey();

            SongName = SongRecord.SongName;
            SongId = SongRecord.SongId;
            SongKey = SongRecord.SongKey;
        }

        public bool LoadRecord()
        {
            if (SongId != default(int))
            {
                SongRecord = GetDataRecordInternalKey();
                return true;
            }

            if (!string.IsNullOrEmpty(SongKey))
            {
                SongRecord = GetDataRecordExternalKey();
                return true;
            }

            return false;
        }

        protected bool Equals(SongData other)
        {
            return string.Equals(SongName, other.SongName) && SongId == other.SongId && string.Equals(SongKey, other.SongKey);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SongName?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ SongId;
                hashCode = (hashCode * 397) ^ (SongKey?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #region IDataStore interface methods
        public WruntEntity Entity { get; set; }
        public void Initialize()
        {
            Entity = new WruntEntity();
        }

        public bool CreateDataRecord()
        {
            try
            {
                SongRecord = InitializeDataRecord(SongRecord);
                SongRecord.DateAdded = DateTime.Now;
                Entity.Songs.Add(SongRecord);

                Entity.SaveChanges();

                SongId = SongRecord.SongId = GetDataRecordExternalKey().SongId;
            }
            catch (Exception e)
            {
                //TODO: log error
                return false;
            }

            return VerifyDataRecord(SongRecord);
        }

        public bool DeleteDataRecord()
        {
            try
            {
                LoadRecord();
                Entity.Songs.Remove(SongRecord);

                Entity.SaveChanges();
            }
            catch (Exception e)
            {
                //TODO: log error
                return false;
            }

            return !VerifyDataRecord(SongRecord);
        }

        public Song InitializeDataRecord()
        {
            return InitializeDataRecord(SongRecord);
        }

        public Song InitializeDataRecord(Song record)
        {
            if (record == null)
                record = new Song();

            record.SongName = SongName;
            record.SongId = SongId;
            record.SongKey = SongKey;

            return record;
        }

        public Song GetDataRecord(Func<Song, bool> match)
        {
            return Entity.Songs.FirstOrDefault(match);
        }

        public Song GetDataRecordInternalKey()
        {
            return GetDataRecord(x => x.SongId == SongId);
        }

        public Song GetDataRecordExternalKey()
        {
            return GetDataRecord(x => x.SongKey == SongKey);
        }

        public bool VerifyDataRecord(Song record)
        {
            var rec = GetDataRecord(x => x.SongId == SongId && x.SongKey == SongKey && x.SongName == SongName);
            return rec != null;
        }
        #endregion
        #region IJsonSerializable interface methods
        public SongData ToObject(string jsonData)
        {
            return ToSong(jsonData);
        }

        public static SongData ToSong(string jsonData)
        {
            return JsonConvert.DeserializeObject<SongData>(jsonData);
        }

        public string ToJsonString()
        {
            return ToJsonString(this);
        }

        public string ToJsonString(SongData obj)
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}