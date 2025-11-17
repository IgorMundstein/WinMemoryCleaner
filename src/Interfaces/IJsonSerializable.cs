namespace WinMemoryCleaner
{
    /// <summary>
    /// Represents an object that can be serialized to JSON.
    /// </summary>
    public interface IJsonSerializable
    {
        /// <summary>
        /// Converts the object to a JSON-serializable representation.
        /// </summary>
        /// <returns>An object ready for JSON serialization.</returns>
        object ToJson();
    }
}
