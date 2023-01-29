using Services.Interfaces;

namespace Backend.Utils;

public class TokenUtil
{
    private readonly ITokenService _tokenService;

    public TokenUtil(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public int GetUserId(HttpContext context)
    {
        var tokenFromRequest = context.Request.Headers["Authorization"];
        if (tokenFromRequest.Count > 0)
        {
            var token = tokenFromRequest[0]?.Split(" ")[1];
            if (token != null)
            {
                return _tokenService.GetUserId(token);
            }
            return -1;
        }
        return -1;
    }
}