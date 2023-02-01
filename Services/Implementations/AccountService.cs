using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using DTOs.Requests;
using DTOs.Responses;
using Services.Interfaces;
using Services.Utils;

namespace Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IRepository<BankUser> _accountsRepository;
    private readonly ITokenService _tokenService;
    private readonly ITransferRepository _transferRepository;

    public AccountService(ITokenService tokenService, IRepository<BankUser> userRepository, ITransferRepository transferRepository)
    {
        _accountsRepository = userRepository;
        _tokenService = tokenService;
        _transferRepository = transferRepository;
    }
    
    public async Task<ApiResultBase<string>> RegisterUser(LoginCredentials registerCredentials)
    {
        var existingUser = await _accountsRepository.GetByPredicate(user => user.Login.Equals(registerCredentials.Login));
        if (existingUser != null)
        {
            return new ApiResultBase<string>()
            {
                IsSuccess = false,
                Errors = $"Login {registerCredentials.Login} is already taken"
            };
        }
        
        string salt = RandomStringGenerator.GenerateRandomString(1);
        string passhash = Sha256HashGenerator.ComputeSha256Hash(registerCredentials.Password + salt);
        var newUser = new BankUser()
        {
            Login = registerCredentials.Login,
            Passhash = passhash,
            Salt = salt,
            AccountCreated = DateTime.Now,
            Account = 0,
            SpendingLimit = null
        };
        await _accountsRepository.Add(newUser);
        string token = _tokenService.GenerateToken(newUser);
        return new ApiResultBase<string>()
        {
            IsSuccess = true,
            Body = token
        };
    }

    public async Task<ApiResultBase<string>> LoginUser(LoginCredentials loginCredentials)
    {
        var existingUser = await _accountsRepository.GetByPredicate(user => user.Login.Equals(loginCredentials.Login));
        if (existingUser == null)
        {
            return new ApiResultBase<string>()
            {
                IsSuccess = false,
                Errors = "Wrong login or password"
            };
        }

        var passhash = Sha256HashGenerator.ComputeSha256Hash(loginCredentials.Password + existingUser.Salt);
        if (passhash != existingUser.Passhash)
        {
            return new ApiResultBase<string>()
            {
                IsSuccess = false,
                Errors = "Wrong login or password"
            };
        }

        string token = _tokenService.GenerateToken(existingUser);
        return new ApiResultBase<string>()
        {
            IsSuccess = true,
            Body = token
        };
    }


    public async Task<ApiResultBase<AccountDetails>> GetAccountInfo(int userId)
    {
        var user = await _accountsRepository.GetById(userId);
        if (user == null)
        {
            return new ApiResultBase<AccountDetails>()
            {
                IsSuccess = false,
                Errors = "User does not exists"
            };
        }
        
        var result = new AccountDetails()
        {
            Login = user.Login,
            Permissions = user.UserType.ToString(),
            RegisterTime = user.AccountCreated,
            DailySpendingLimit = user.SpendingLimit,
            Balance = await _transferRepository.GetCurrentUserAccount(userId),
            TodayAccountChange = await _transferRepository.GetUserTodaySpending(userId)
        };
        
        return new ApiResultBase<AccountDetails>()
        {
            IsSuccess = true,
            Body = result
        };
    }

    public async Task<ApiResultBase<bool>> EditAccount(int userId, ChangeSettingsRequest accountCreds)
    {
        var user = await _accountsRepository.GetById(userId);
        if (user == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Account does not exists"
            };
        }

        user.SpendingLimit = accountCreds.NewDailyLimit;
        await _accountsRepository.Update(user);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }
}