using System.Net;
using Backend.Utils;
using DTOs.Requests;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Backend.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/[action]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly TokenUtil _tokenService;
    public AccountsController(IAccountService accountService, TokenUtil tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResultBase<string>>> Register(LoginCredentials registerCredentials)
    {
        var registerResult = await _accountService.RegisterUser(registerCredentials);
        return registerResult;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResultBase<string>>> Login(LoginCredentials loginCredentials)
    {
        var loginResult = await _accountService.LoginUser(loginCredentials);
        return loginResult;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<AccountDetails>>> AccountDetails()
    {
        int userId = _tokenService.GetUserId(HttpContext);
        return await _accountService.GetAccountInfo(userId);
    }

    [HttpPut]
    public async Task<ActionResult<ApiResultBase<bool>>> EditAccount(ChangeSettingsRequest newcreds)
    {
        int userId = _tokenService.GetUserId(HttpContext);
        return await _accountService.EditAccount(userId, newcreds);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<AccountSettings>>> Settings()
    {
        int userId = _tokenService.GetUserId(HttpContext);
        return await _accountService.GetAccountSettings(userId);
    }
}