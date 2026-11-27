namespace MiniAdmin.Core.Members;

public interface IMemberRepository
{
    Task AddAsync(Member member);
    Task<Member> GetByIdAsync(int id);
    Task<List<Member>> AllAsync();
    Task UpdateAsync(Member member);
    Task DeleteAsync(int id);
}
