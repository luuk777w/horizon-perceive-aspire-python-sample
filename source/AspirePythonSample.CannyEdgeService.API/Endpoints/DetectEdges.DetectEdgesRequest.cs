namespace AspirePythonSample.CannyEdgeService.API.Endpoints;

public class DetectEdgesRequest
{
    public IFormFile Image { get; set; } = default!;

    public string Filename { get; set; } = default!;

    public int MinThreshold { get; set; } = default!;

    public int MaxThreshold { get; set; } = default!;
}
