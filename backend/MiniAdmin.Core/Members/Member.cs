

namespace MiniAdmin.Core.Members;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Nickname { get; set; }

    public Member(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public string Code()
    {
        return MemberFormat.MemberCode(Id);
    }

    public virtual bool CanManageMembers()
    {
        return false;
    }
}

