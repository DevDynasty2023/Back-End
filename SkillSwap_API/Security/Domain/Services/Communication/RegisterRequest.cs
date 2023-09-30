using System.ComponentModel.DataAnnotations;

namespace SkillSwap_API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string profilePhoto { get; set; }
    [Required]
    public int skillCoins { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int RoleId { get; set; }
}