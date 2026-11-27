namespace MiniAdmin.Core.Products;

public static class ProductIds
{
    public static int Get(int[] ids, int index)
    {
        return ids[index];
    }

    public static int Last(int[] ids)
    {
        return ids[ids.Length - 1];
    }
}
