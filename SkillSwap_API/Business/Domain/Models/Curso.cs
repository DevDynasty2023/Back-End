using SkillSwap_API.Security.Domain.Models;

namespace SkillSwap_API.Business.Domain.Models;

public class Curso
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public IList<Leccion> Lecciones { get; set; } = new List<Leccion>(); 
}