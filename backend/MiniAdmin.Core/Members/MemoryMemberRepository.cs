namespace MiniAdmin.Core.Members;

public class MemoryMemberRepository : IMemberRepository
{
    private List<Member> _items = new List<Member>();

    public Task AddAsync(Member member)
    {
        _items.Add(member);
        return Task.CompletedTask;
    }

    public Task<Member> GetByIdAsync(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Id == id)
            {
                return Task.FromResult(_items[i]);
            }
        }
        throw new KeyNotFoundException("找不到會員");
    }

    public Task<List<Member>> AllAsync()
    {
        return Task.FromResult(_items);
    }

    public Task UpdateAsync(Member member)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Id == member.Id)
            {
                _items[i].Name = member.Name;
                _items[i].Email = member.Email;
                return Task.CompletedTask;
            }
        }
        throw new KeyNotFoundException("找不到會員");
    }

    public Task DeleteAsync(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Id == id)
            {
                _items.RemoveAt(i);
                return Task.CompletedTask;
            }
        }
        throw new KeyNotFoundException("找不到會員");
    }
}
