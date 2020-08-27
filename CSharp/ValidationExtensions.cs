public static class ValidationExtensions
{
    /// <summary>
    /// Returs true if string is whitespace, null or empty
    /// </summary>
    /// <param name="str">string input</param>
    /// <returns>boolean value if string is empty or not</returns>
    public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str) || string.Empty == str;

    /// <summary>
    /// Checks if guid is valid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static bool IsValidGuid(this Guid guid) => guid != null && guid != Guid.Empty;

    /// <summary>
    /// Checks if filename is valid
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool IsValidFilename(this string fileName)
    {
        if (fileName.IsEmpty()) return false;

        string strTheseAreInvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
        Regex regInvalidFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");

        if (regInvalidFileName.IsMatch(fileName)) { return false; };

        return true;
    }

    /// <summary>
    /// Checks if T is Serializable
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsSerializable<T>(this T obj)
    {
        Type type = obj.GetType();

        return type.IsSerializable;
    }

}