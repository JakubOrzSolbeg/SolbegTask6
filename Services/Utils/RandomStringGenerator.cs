using System.Text;

namespace Services.Utils;

public class RandomStringGenerator
{
    public static string GenerateRandomString(int iterations = 1)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iterations; i++)
        {
            sb.Append(Guid.NewGuid().ToString("N"));
        }
        return sb.ToString();
    }
}