namespace AiModelOrchestrationSolution.Web.Services;

public class ImageService(IWebHostEnvironment env)
{
    private readonly IWebHostEnvironment _env = env;

    public List<string> GetImagesFromFolder(string folderName = "Uploads")
    {
        var folderPath = Path.Combine(_env.WebRootPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            return [];
        }

        return Directory.GetFiles(folderPath)
            .Where(f => new[] { ".jpg", ".jpeg", ".png" }.Contains(
                Path.GetExtension(f).ToLower()))
            .Select(Path.GetFileName)
            .OrderBy(f => f)
            .ToList()!;
    }
}