using System.Security.Cryptography;
using System.Text;

namespace Services.Utils;

public class Sha256HashGenerator
{
    public static string ComputeSha256Hash(string rawData)  
    {
        using (SHA256 sha256Hash = SHA256.Create())  
        {  
                
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();  
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();  
        }  
    } 
}