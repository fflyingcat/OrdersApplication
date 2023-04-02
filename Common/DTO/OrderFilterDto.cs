namespace Common.DTO;

public class OrderFilterDto
{
    public List<string> OrderNumbers { get; set; }
    public List<ProviderDto> Providers { get; set; }
}