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
    public static bool IsEmptyOrNullGuid(this Guid guid) => guid != null && guid != Guid.Empty;

    /// <summary>
    /// Checks if string can be parsed to guid and is valid
    /// </summary>
    /// <param name="guidStr"></param>
    /// <returns></returns>
    public static bool IsStringEmptyOrNullGuid(this string guidStr) 
    {
        var guidValue = new Guid(guidStr);
        return !guidStr.IsEmpty() && guidValue != Guid.Empty;
    }

    /// <summary>
    /// Better way to check if the given guid is valid, only .NET Core
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static bool IsValidGuid(this Guid guid) => Guid.TryParse(guid.ToString(), out Guid _);

    /// <summary>
    /// Better way to check if the given string can be parsed to Guid, only .NET Core
    /// </summary>
    /// <param name="guidStr"></param>
    /// <returns></returns>
    public static bool IsStringValidGuid(this string guidStr) => Guid.TryParse(guidStr, out Guid _);

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


    /// <summary>
    /// Checks if input is a valid email adress format
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string email)
    {
        if (email.IsEmpty())
            return false;

        // Examines the domain part of the email and normalizes it.
        string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            var domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if the input is a valid Albanian NID or NIPT
    /// </summary>
    /// <param name="nid"></param>
    /// <returns></returns>
    public static bool IsValidNID(this string nid)
    {
        if (nid.IsEmpty())
            return false;

        if (nid.Length != 10)
            return false;

        try
        {
            return Regex.IsMatch(nid,
                @"^[a-zA-Z]\d{8}[a-zA-Z]",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

}