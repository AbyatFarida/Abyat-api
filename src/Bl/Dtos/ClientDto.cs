using Abyat.Bl.Dtos.Base;

namespace Abyat.Bl.Dtos;

public class ClientDto : BaseDto
{
    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public Guid? CompanyId { get; set; }

}