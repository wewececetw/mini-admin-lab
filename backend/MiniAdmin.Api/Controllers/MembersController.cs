using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniAdmin.Core.Members;

namespace MiniAdmin.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _repo;

    public MembersController(IMemberRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public Task<List<Member>> GetAll()
    {
        return _repo.AllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetById(int id)
    {
        // 當_repo.GetByIdAsync(id)有值 回傳 _repo.GetByIdAsync(id)
        //else  return NotFound();
        try
        {
            return await _repo.GetByIdAsync(id);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Member>> Create(Member member)
    {
        await _repo.AddAsync(member);
        return Created("", member);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Member>> Update(int id, Member member)
    {
        try
        {
            member.Id = id;
            await _repo.UpdateAsync(member);
            return Ok(member);
        }
        catch (System.Exception)
        {

            return NotFound();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
        catch (System.Exception)
        {

            return NotFound();
        }
    }
}
