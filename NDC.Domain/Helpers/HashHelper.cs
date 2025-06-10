using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using NDC.Domain.Models;

namespace NDC.Domain.Helpers;

public static class HashHelper
{
    public static string ComputeHash(MeteoriteDto item)
    {
        var json = JsonSerializer.Serialize(item);
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(json);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
