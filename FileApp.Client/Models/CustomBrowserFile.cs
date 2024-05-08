using Microsoft.AspNetCore.Components.Forms;

public class CustomBrowserFile
{
    private readonly IBrowserFile _browserFile;

    public CustomBrowserFile(IBrowserFile browserFile)
    {
        _browserFile = browserFile;
    }

    public IBrowserFile File => _browserFile;

    // Additional property
    public string? TempName { get; set; }
}
