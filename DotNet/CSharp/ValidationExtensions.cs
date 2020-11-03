using System.Collections;
using System.Collections.Generic;

public static class ValidationExtensions
{

    /// <summary>
    ///  Generic method that returns true if the collection is empty, otherwise false
    /// </summary>
    /// <typeparam name="T">Collection</typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this T collection) where T : ICollection
                        => collection.Count == 0;

    /// <summary>
    /// Checks if guid is valid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static bool IsEmptyGuidOrNull(this Guid guid)
                        => guid == null && guid == Guid.Empty;

    /// <summary>
    /// Checks if string can be parsed to guid and is valid
    /// </summary>
    /// <param name="guidStr"></param>
    /// <returns></returns>
    public static bool IsStringEmptyGuidOrNull(this string guidStr)
    {
        var guidValue = new Guid(guidStr);
        return guidStr.IsEmptyWhitespaceOrNull() && guidValue == Guid.Empty;
    }

    /// <summary>
    /// Checks if the datetime is null or default (empty: 1/1/0001 ...);
    /// </summary>
    /// <param name="date">DateTime</param>
    /// <returns>true if the date is empty</returns>
    public static bool IsEmptyDate(this DateTime date)
                        => date == null || date == default(DateTime);

    /// <summary>
    /// Returs true if string is whitespace, null or empty
    /// </summary>
    /// <param name="str">string input</param>
    /// <returns>boolean value if string is empty or not</returns>
    public static bool IsEmptyWhitespaceOrNull(this string str)
                        => string.IsNullOrWhiteSpace(str) || string.Empty == str;

    
    /// <summary>
    /// Better way to check if the string is valid guid, only .NET Core
    /// </summary>
    /// <param name="inputString"></param>
    /// <returns></returns>
    public static bool IsValidGuid(this string inputString)
    {
        Guid.TryParse(inputString, out var guidOutput);
        return guidOutput != Guid.Empty;
    }

    /// <summary>
    /// Better way to check if the given string can be parsed to Guid, only .NET Core
    /// </summary>
    /// <param name="guidStr"></param>
    /// <returns></returns>
    public static bool IsStringValidGuid(this string guidStr)
                        => Guid.TryParse(guidStr, out Guid _);

    /// <summary>
    /// Checks if filename is valid
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static bool IsValidFilename(this string fileName)
    {
        if (fileName.IsEmptyWhitespaceOrNull()) return false;

        string strTheseAreInvalidFileNameChars = new string(System.IO.Path.GetInvalidFileNameChars());
        Regex regInvalidFileName = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");

        if (regInvalidFileName.IsMatch(fileName)) { return false; };

        return true;
    }

    /// <summary>
    /// Checks if URL is valid
    /// </summary>
    /// <param name="uriName"></param>
    /// <returns></returns>
    public static bool IsValidUrl(this string uriName)
    {
        Uri uriResult;
        bool result = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        return result;
    }

        public static bool IsLocalPath(this string path)
        {
            bool ret = true;
            try
            {
                ret = new Uri(path).IsFile;
            }
            catch
            {
                if (path.StartsWith("http://") ||
                    path.StartsWith(@"http:\\") ||
                    path.StartsWith("https://") ||
                    path.StartsWith(@"https:\\"))
                {
                    return false;
                }
            }

            return ret;
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
        if (email.IsEmptyWhitespaceOrNull())
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
        if (nid.IsEmptyWhitespaceOrNull())
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