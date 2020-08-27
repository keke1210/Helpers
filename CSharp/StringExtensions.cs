/// <summary>
/// Returs true if string is whitespace, null or empty
/// </summary>
/// <param name="str">string input</param>
/// <returns>boolean value if string is empty or not</returns>

public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str) || string.Empty == str;