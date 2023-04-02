namespace Common.DTO;

public class OrderItemOutDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
}