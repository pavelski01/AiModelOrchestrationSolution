namespace AiModelOrchestrationSolution.Web.Model;

public class Receipt
{
    public List<LineItem> Items { get; set; } = [];

    public decimal Subtotal { get; set; }
}
