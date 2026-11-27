namespace MiniAdmin.Core.Products;

public static class Pricing
{
    public const decimal TaxRate = 0.05m;

    public static decimal WithTax(decimal price)
    {
        return WithTax(price, TaxRate);
    }

    public static decimal WithTax(decimal price, decimal rate)
    {
        return price * (1 + rate);
    }



    public static int ParseQuantity(string text)
    {
        return int.Parse(text);
    }

    public static int WholeDollars(decimal price)
    {
        return (int)price;
    }

    public static double AverageRating(int totalStars, int count)
    {
        return (double)totalStars / count;
    }

    public static string FormatPrice(decimal price)
    {

        return $"NT$ {price:N0}";
    }

    public static decimal ShippingFee(decimal orderTotal, decimal threshold = 1000m)
    {

        if (orderTotal >= threshold)
        {
            return 0m;
        }
        else
        {
            return 60m;
        }
    }
}
