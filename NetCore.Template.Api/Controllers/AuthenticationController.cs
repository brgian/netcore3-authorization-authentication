using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Template.DTOs.Requests;
using NetCore.Template.DTOs.Responses;
using NetCore.Template.Services;

namespace NetCore.Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult ValidateCredentials([FromBody]LoginCredentials loginCredentials)
        {
            TokenResponse tokenResponse;

            if (authenticationService.TryAuthenticate(loginCredentials, out tokenResponse))
                return Ok(tokenResponse);

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("test-token")]
        public IActionResult TestToken()
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("test-admin-token")]
        public IActionResult TestAdminToken()
        {
            return Ok();
        }

        [Authorize(Roles = "User")]
        [HttpGet("test-user-token")]
        public IActionResult TestUserToken()
        {
            return Ok();
        }
    }
}