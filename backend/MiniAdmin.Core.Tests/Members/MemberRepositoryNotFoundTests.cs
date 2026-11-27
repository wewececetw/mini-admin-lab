using MiniAdmin.Core.Members;

namespace MiniAdmin.Core.Tests.Members;

public class MemberRepositoryNotFoundTests
{
    [Fact]
    public async Task GetByIdAsync_找不到就丟例外()
    {
        IMemberRepository repo = new MemoryMemberRepository();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.GetByIdAsync(99));
    }
}
