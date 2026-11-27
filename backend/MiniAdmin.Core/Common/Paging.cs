namespace MiniAdmin.Core.Common;

public static class Paging
{
    public static int PageCount(int totalItems, int pageSize)
    {
        if (totalItems % pageSize > 0)
        {
            return (totalItems / pageSize) + 1;
        }
        else
        {
            return (totalItems / pageSize);
        }
    }

    public static PagedResult<T> CreatePage<T>(List<T> all, int page, int pageSize)
    {
        int start = (page - 1) * pageSize;
        PagedResult<T> result = new PagedResult<T>();

        for (int i = start; i < all.Count && i < start + pageSize; i++)
        {
            result.Items.Add(all[i]);
        }

        result.TotalCount = all.Count;
        result.Page = page;
        result.PageSize = pageSize;
        return result;
    }

    public static List<T> Filter<T>(List<T> all, Func<T, bool> keep)
    {
        List<T> kept = new List<T>();
        for (int i = 0; i < all.Count; i++)
        {
            if (keep(all[i]))
            {
                kept.Add(all[i]);
            }
        }
        return kept;
    }
}
