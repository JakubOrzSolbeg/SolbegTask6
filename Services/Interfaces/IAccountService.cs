using DTOs.Requests;
using DTOs.Responses;

namespace Services.Interfaces;

public interface IAccountService
{
    public Task<ApiResultBase<string>> RegisterUser(LoginCredentials registerCredentials);
    public Task<ApiResultBase<string>> LoginUser(LoginCredentials loginCredentials);
    public Task<ApiResultBase<AccountDetails>> GetAccountInfo(int userId);
}