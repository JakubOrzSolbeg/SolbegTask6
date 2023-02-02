using DTOs.Requests;
using DTOs.Responses;

namespace Services.Interfaces;

public interface IAccountService
{
    public Task<ApiResultBase<string>> RegisterUser(LoginCredentials registerCredentials);
    public Task<ApiResultBase<string>> LoginUser(LoginCredentials loginCredentials);
    public Task<ApiResultBase<AccountDetails>> GetAccountInfo(int userId);
    public Task<ApiResultBase<bool>> EditAccount(int userId, ChangeSettingsRequest accountCreds);
    public Task<ApiResultBase<AccountSettings>> GetAccountSettings(int userId);
}