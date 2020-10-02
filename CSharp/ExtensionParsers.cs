public static class ExtensionParsers
{
    /// <summary>
    /// Parse string to integer
    /// </summary>
    /// <param name="str"></param>
    /// <returns>parsed value</returns>
    public static int ToInt(this string str)
    {
        int result;
        int.TryParse(str, out result);
        return result;
    }

    /// <summary>
    /// Parse string to double
    /// </summary>
    /// <param name="str"></param>
    /// <returns>parsed value</returns>
    public static double ToDouble(this string str)
    {
        double result;
        double.TryParse(str, out result);
        return result;
    }

    /// <summary>
    /// Parse string to Datetime
    /// </summary>
    /// <param name="str"></param>
    /// <returns>parsed value</returns>
    public static DateTime ToDateTime(this string str)
    {
        DateTime result;
        DateTime.TryParse(str, out result);
        return result;
    }

    /// <summary>
    /// Parse from double to Datetime
    /// </summary>
    /// <param name="dateAsNumber"></param>
    /// <returns>parsed value</returns>
    public static DateTime FromDoubleToDateTime(this double dateAsNumber)
    {
        try
        {
            return DateTime.FromOADate(dateAsNumber);
        }
        catch (Exception)
        {
            return default(DateTime);
        }
    }
}
