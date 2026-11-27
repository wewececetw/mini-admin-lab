using System.Text;
namespace MiniAdmin.Core.Members;

public static class MemberFormat
{
    public static string MemberCode(int id)
    {
        return $"M{id:D4}";
    }

    public static string MaskEmail(string email)
    {
        int mouse_num = email.IndexOf("@");

        string two_word_str = email.Substring(0, 2);

        string email_str = email.Substring(mouse_num);

        int mark_num = mouse_num - 2;
        return two_word_str + new string('*', mark_num) + email_str;
    }

    public static string BuildCard(int id, string name, string email)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"會員編號：{MemberCode(id)}");
        sb.AppendLine($"姓名：{name}");
        sb.AppendLine($"Email：{MaskEmail(email)}");
        return sb.ToString();
    }
}
