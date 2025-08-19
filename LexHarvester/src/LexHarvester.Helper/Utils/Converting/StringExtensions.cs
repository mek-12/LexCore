namespace LexHarvester.Helper.Utils.Converting;

public static class StringExtensions
{
    public static byte[] Base64ToBytes(this string base64)
    {
        if (string.IsNullOrEmpty(base64))
        {
            return Array.Empty<byte>();
        }
        // Maksimum gereken buffer büyüklüğü hesaplanır
        byte[] buffer = new byte[base64.Length * 3 / 4];

        // Dönüştürme başarılıysa, sadece yazılan kısmı al
        if (Convert.TryFromBase64String(base64, buffer, out int bytesWritten))
        {
            byte[] result = new byte[bytesWritten];
            Array.Copy(buffer, result, bytesWritten);
            return result;
        }

        // Başarısızsa null veya boş dizi dönebilir
        return Array.Empty<byte>();
    }
}
