using System;
namespace ECommerceAPI.Infrastructure.Utilities;

public static class DateUtility
{
    public static string GetCurrentDateTime()
    {
        return DateTime.Now.ToString("ddMMyyyyHHmmsss");
    }
}


