using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Shared.Domain.Services.Communication;

namespace SkillSwap_API.Business.Domain.Services.Communication;

public class LeccionResponse: BaseResponse<Leccion>
{
    public LeccionResponse(string message) : base(message)
    {
    }

    public LeccionResponse(Leccion model) : base(model)
    {
    }
}