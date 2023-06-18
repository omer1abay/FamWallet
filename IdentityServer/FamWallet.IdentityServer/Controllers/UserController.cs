using FamWallet.IdentityServer.DTOs;
using FamWallet.IdentityServer.Models;
using FamWallet.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace FamWallet.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserManager<ApplicationUser> userManager { get; set; }
        public IGroupService _groupService { get; set; }

        public UserController(UserManager<ApplicationUser> userManager, IGroupService service)
        {
            this.userManager = userManager;
            _groupService = service;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            Random randomWalletNumber = new Random();
            int walletNumber = randomWalletNumber.Next(100000,999999);

            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                WalletNumber = walletNumber
            };

            var response = await userManager.CreateAsync(user,signupDto.Password);

            if (!response.Succeeded)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null)
            {
                BadRequest();
            }

            var user = userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null)
            {
                BadRequest();
            }

            return Ok(user);

        }


        [HttpGet]
        public async Task<IActionResult> GetUserAll()
        {

            var users = userManager.Users.ToList();

            return Ok(users);

        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(GroupModel model)
        {
            _groupService.AddGroup(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroup()
        {
            var result = _groupService.GetAllGroup();
            return Ok(result);
        } 

    }
}
