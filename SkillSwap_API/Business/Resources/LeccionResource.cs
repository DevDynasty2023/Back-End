namespace SkillSwap_API.Business.Resources;

public class LeccionResource
{
    public int Id { get; set; }
    public string titulo { get; set; }
    public string contenido { get; set; }
    public int CursoId { get; set; }
}