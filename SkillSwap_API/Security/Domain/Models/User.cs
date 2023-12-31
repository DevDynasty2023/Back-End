﻿using System.Text.Json.Serialization;

namespace SkillSwap_API.Security.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string profilePhoto { get; set; }
    public int SkillCoins { get; set; }
    public int RoleId { get; set; }
    //Relationship
    public Role Role { get; set; }
    
    [JsonIgnore]
    public string PasswordHash { get; set; }
}