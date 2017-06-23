namespace Wruntisms.Repository.DAL
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using log4net;
    using Newtonsoft.Json;

    /// <summary>
    /// Data Access Layer for Song database storage
    /// </summary>
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

        private static readonly ILog LogObj = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            if (SongRecord.Deleted)
            {
                if (SongId != default(int))
                {
                    SongRecord = GetDeletedDataRecordInternalKey();
                    return true;
                }

                if (!string.IsNullOrEmpty(SongKey))
                {
                    SongRecord = GetDeletedDataRecordExternalKey();
                    return true;
                }
            }

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
                LogObj.Error($"{e.GetType().Name}: error creating record", e);
                return false;
            }

            return VerifyDataRecord(SongRecord);
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
            return Entity.Songs.Where(match).FirstOrDefault(x => !x.Deleted);
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

        public bool MarkDataRecordDeleted()
        {
            try
            {
                LoadRecord();
                SongRecord.Deleted = true;

                Entity.SaveChanges();
            }
            catch (Exception e)
            {
                LogObj.Error($"{e.GetType().Name}: error setting record as deleted", e);
                return false;
            }

            return GetDeletedDataRecord(x => x.SongId == SongId && x.SongKey == SongKey && x.SongName == SongName) != null;
        }

        public Song GetDeletedDataRecord(Func<Song, bool> match)
        {
            return Entity.Songs.Where(match).FirstOrDefault(x => x.Deleted);
        }

        public Song GetDeletedDataRecordInternalKey()
        {
            return GetDeletedDataRecord(x => x.SongId == SongId);
        }

        public Song GetDeletedDataRecordExternalKey()
        {
            return GetDeletedDataRecord(x => x.SongKey == SongKey);
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
                LogObj.Error($"{e.GetType().Name}: error deleting data record", e);
                return false;
            }

            return !VerifyDataRecord(SongRecord);
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