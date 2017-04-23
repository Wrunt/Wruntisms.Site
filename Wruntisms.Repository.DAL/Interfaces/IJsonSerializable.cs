namespace Wruntisms.Repository.DAL
{
    using Newtonsoft.Json;

    /// <summary>
    /// Enforces consistent naming for JSON Serialization
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    internal interface IJsonSerializable<T>
    {
        /// <summary>
        /// Deserializes JSON to object
        /// </summary>
        /// /// <param name="jsonData">JSON data of object to deserialize</param>
        /// <returns>Object taken from JSON</returns>
        T ToObject(string jsonData);
        /// <summary>
        /// Serializes to JSON from object
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns></returns>
        string ToJsonString(T obj);
    }
}
