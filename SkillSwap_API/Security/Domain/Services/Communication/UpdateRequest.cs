using System.ComponentModel.DataAnnotations;

namespace SkillSwap_API.Security.Domain.Services.Communication;

public class UpdateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string profilePhoto { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int SkillCoins { get; set; }
    public int RoleId { get; set; }
}