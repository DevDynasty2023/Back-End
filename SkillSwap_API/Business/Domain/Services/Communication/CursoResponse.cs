using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Shared.Domain.Services.Communication;

namespace SkillSwap_API.Business.Domain.Services.Communication;

public class CursoResponse: BaseResponse<Curso>
{
    public CursoResponse(string message) : base(message)
    {
    }

    public CursoResponse(Curso model) : base(model)
    {
    }
}