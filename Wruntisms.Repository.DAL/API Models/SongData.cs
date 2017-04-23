namespace Wruntisms.Repository.DAL
{
    using System;
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
            Initialize();

            SongRecord = GetDataRecordInternalKey();

            SongName = SongRecord.SongName;
            SongId = SongRecord.SongId;
            SongKey = SongRecord.SongKey;
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

            }
            catch (Exception e)
            {
                //TODO: log error
                return false;
            }

            return GetDataRecordInternalKey() == null;
        }

        public Song InitializeDataRecord()
        {
            return InitializeDataRecord(new Song());
        }

        public Song InitializeDataRecord(Song record)
        {
            throw new System.NotImplementedException();
        }

        public Song GetDataRecord(Action matchAction)
        {
            throw new System.NotImplementedException();
        }

        public Song GetDataRecordInternalKey()
        {
            throw new System.NotImplementedException();
        }

        public Song GetDataRecordExternalKey()
        {
            throw new System.NotImplementedException();
        }

        public bool VerifyDataRecord(Song record)
        {
            throw new NotImplementedException();
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