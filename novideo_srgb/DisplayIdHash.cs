using System;
using System.Text;

internal static class DisplayIdHash
{
    public static string GetFilename(uint displayId)
    {
        var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = BitConverter.GetBytes(displayId);
        var hash = sha.ComputeHash(bytes);

        var sb = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
            sb.Append(b.ToString("x2"));

        return sb.ToString() + ".bin";
    }
}