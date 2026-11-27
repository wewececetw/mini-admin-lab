namespace MiniAdmin.Core.Members;

public static class PasswordStrength
{
    public static string Check(string password)
    {
        bool isbool = false;
        for (int i = 0; i < password.Length; i++)
        {
            if (isbool == false)
            {
                isbool = char.IsDigit(password[i]);

            }
        }

        if (password.Length < 8)
        {
            return "弱";
        }
        else if (password.Length >= 8 && isbool == true)
        {
            return "強";
        }
        else
        {
            return "中";
        }
    }
}
