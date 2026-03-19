using Json.Schema;
using System.Text.Json;

namespace AiModelOrchestration.Web.Validators;

public class JsonValidator(string schema)
{
    public readonly JsonSchema _schema = 
        JsonSerializer.Deserialize<JsonSchema>(schema) 
        ?? throw new InvalidDataException("Schema must be provided");

    public (bool IsValid, IList<string> Errors) Validate(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var results = _schema.Evaluate(doc.RootElement);

            var errors = (results.Details ?? Enumerable.Empty<EvaluationResults>())
                .SelectMany(
                    evaluationResults =>
                    evaluationResults.Errors?.Select(e => $"{evaluationResults.InstanceLocation}: {e.Value}") ?? []
                )
                .Where(error => !string.IsNullOrEmpty(error))
                .ToList();

            return (results.IsValid, errors);
        }
        catch (JsonException ex)
        {
            return (false, new List<string> { $"Invalid JSON: {ex.Message}" });
        }
    }
}
