using System.Text;

namespace ECommerceAPI.Infrastructure.Utilities;

public static class NameUtility
{
    public static string RemoveNonAlphabeticCharacters(string name)
    {
        var loweredReplacedName = name.ToLower().Replace("-", "").Replace(" ", "-");

        var stringBuilder = new StringBuilder();

        foreach (char c in loweredReplacedName)
        {
            if ((c >= 'a' && c <= 'z') || c == '-')
            {
                stringBuilder.Append(char.ToLower(c));
            }
        }

        return stringBuilder.ToString();
    }
}


