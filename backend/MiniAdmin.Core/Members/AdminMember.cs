namespace MiniAdmin.Core.Members;

public class AdminMember : Member
{
    public AdminMember(int id, string name, string email)
        : base(id, name, email)
    {
    }

    public override bool CanManageMembers()
    {
        return true;
    }
}
