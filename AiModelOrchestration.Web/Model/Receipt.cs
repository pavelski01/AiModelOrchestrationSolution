namespace AiModelOrchestration.Web.Model;

using System.Text.Json;
using AiModelOrchestration.Web.Validators;

public class Receipt
{
    public List<LineItem> Items { get; set; } = [];

    public decimal Subtotal { get; set; }

    public string ToJson(string? schema = null)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(this, options);

        schema ??= File.ReadAllText("Metadata/receipt-schema.json");

        var validator = new JsonValidator(schema);
        var (isValid, errors) = validator.Validate(json);

        if (!isValid)
        {
            throw new InvalidOperationException($"Receipt JSON validation failed: {string.Join("; ", errors)}");
        }

        return json;
    }
}
