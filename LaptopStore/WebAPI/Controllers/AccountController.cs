using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IPasswordHasher<UserEntity> _passwordHasher;
        private ILogger<AccountController> _logger;
        private IConfiguration _configurationRoot;

        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, RoleManager<IdentityRole> roleManager,
            IPasswordHasher<UserEntity> passwordHasher, ILogger<AccountController> logger, IConfiguration configurationRoot)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _configurationRoot = configurationRoot;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new UserEntity()
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                FristName = registerModel.FristName,
                LastName = registerModel.LastName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("error", err.Description);

            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            } 
            else
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(model.Email);

                    if (user == null)
                    {
                        return BadRequest();
                    }

                    if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaim = await _userManager.GetClaimsAsync(user);
                        _logger.LogInformation(userClaim.ToString());

                        var claims = new[]
                        {
                           new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                           new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        }.Union(userClaim);

                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configurationRoot["JwtSecurityToken:Key"]));
                        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var jwt = new JwtSecurityToken(
                            issuer: _configurationRoot["JwtSecurityToken:Issuer"],
                            audience: _configurationRoot["JwtSecurityToken:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: signingCredentials
                            );

                        var isAdmin = false;
                        
                        if (userClaim.Count > 0)
                        {
                            for (int i = 0; i < userClaim.Count; i ++)
                            {
                                if (userClaim[i].Type == "Admin")
                                {
                                    isAdmin = true;
                                } else
                                {
                                    isAdmin = false;
                                }
                            }
                        } else
                        {
                            isAdmin = false;
                        }



                        return Ok(new
                        {
                            email = user.Email,
                            fullname = user.DisplayName,
                            isadmin = isAdmin,
                            token = new JwtSecurityTokenHandler().WriteToken(jwt),
                            expiration = jwt.ValidTo
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error when creating token: { e }");
                    return StatusCode((int)HttpStatusCode.InternalServerError, $"Error when creating token: {e}");
                }

            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", " + CookieAuthenticationDefaults.AuthenticationScheme)]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest();
            } else
            {
                //var userName = HttpContext.User.Identity.Name;

                await _signInManager.SignOutAsync();

                return Ok();
            }
        }
    }
}