namespace Wruntisms.Repository.DAL
{
    using System;

    /// <summary>
    /// Allows interfacing between API data and Database records
    /// </summary>
    /// <typeparam name="T">Database type</typeparam>
    internal interface IDataStore<T>
    {
        /// <summary>
        /// Database entity
        /// </summary>
        WruntEntity Entity { get; set; }

        /// <summary>
        /// Initializes a new instance, must include Entity initialization
        /// </summary>
        void Initialize();
        /// <summary>
        /// Creates data record in the Database
        /// </summary>
        /// <returns>Boolean success result</returns>
        bool CreateDataRecord();
        /// <summary>
        /// Deletes data record from the Database
        /// </summary>
        /// <returns></returns>
        bool DeleteDataRecord();
        /// <summary>
        /// Initializes a data record
        /// </summary>
        /// <returns></returns>
        T InitializeDataRecord();
        /// <summary>
        /// Initializes a data record
        /// </summary>
        /// <param name="record">base data record</param>
        /// <returns></returns>
        T InitializeDataRecord(T record);
        /// <summary>
        /// Gets data record from Database using passed action
        /// </summary>
        /// <param name="matchAction">Action</param>
        /// <returns>Data record</returns>
        T GetDataRecord(Action matchAction);
        /// <summary>
        /// Gets data record using internal key
        /// </summary>
        /// <returns>Data record</returns>
        T GetDataRecordInternalKey();
        /// <summary>
        /// Gets data record using 
        /// </summary>
        /// <returns></returns>
        T GetDataRecordExternalKey();
        /// <summary>
        /// Verifies data record in Database matches passed data record
        /// </summary>
        /// <param name="record">Data record against which to check</param>
        /// <returns>True if match, false if no match</returns>
        bool VerifyDataRecord(T record);
    }
}
