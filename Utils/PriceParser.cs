using System.Globalization;

namespace AutoTestsForApplications.Utils;

public static class PriceParser
{
    // "$29.99" -> 29.99m; InvariantCulture, чтобы результат не зависел от региональных настроек ОС
    public static decimal Parse(string? priceText)
    {
        if (string.IsNullOrWhiteSpace(priceText))
        {
            throw new InvalidOperationException("Текст цены пустой или отсутствует.");
        }

        return decimal.Parse(priceText.Trim().TrimStart('$'), CultureInfo.InvariantCulture);
    }
}
