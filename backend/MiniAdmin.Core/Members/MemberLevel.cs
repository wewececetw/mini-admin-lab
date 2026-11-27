namespace MiniAdmin.Core.Members;

public static class MemberLevel
{
    public static string LevelName(int level)
    {
        switch (level)
        {
            case 1:
                return "銅";
            case 2:
                return "銀";
            case 3:
                return "金";
            default:
                return "未知";
        }
    }

    public static string LevelName(MemberTier tier)
    {
        switch (tier)
        {
            case MemberTier.Bronze:
                return "銅";
            case MemberTier.Silver:
                return "銀";
            case MemberTier.Gold:
                return "金";
            default:
                return "未知";
        }
    }
}

