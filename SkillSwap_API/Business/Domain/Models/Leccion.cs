namespace SkillSwap_API.Business.Domain.Models;

public class Leccion
{
    public int Id { get; set; }
    public string titulo { get; set; }
    public string contenido { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }
}