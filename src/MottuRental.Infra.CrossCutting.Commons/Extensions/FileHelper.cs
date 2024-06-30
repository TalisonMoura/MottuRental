using Microsoft.AspNetCore.Http;

namespace MottuRental.Infra.CrossCutting.Commons.Extensions;

public static class FileHelper
{
    public static async Task<byte[]> ToByteAsync(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public static void CreateFolder(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    public async static Task SetImageInFolderAsync(string path, string imageName, byte[] imageData)
    {
        await File.WriteAllBytesAsync(Path.Combine(path, imageName), imageData);
    }

    public async static Task<byte[]> GetImageByNameAsync(string path, string imageName)
    {
        var imagePath = Path.Combine(path, imageName);
        return File.Exists(imagePath) ? await File.ReadAllBytesAsync(imagePath) : default;
    }
}
