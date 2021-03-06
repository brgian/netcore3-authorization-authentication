﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Template.DTOs.Requests;
using NetCore.Template.DTOs.Responses;
using NetCore.Template.Services;

namespace NetCore.Template.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("validate-credentials")]
        public IActionResult ValidateCredentials([FromBody]LoginCredentials loginCredentials)
        {
            if (authenticationService.TryAuthenticate(loginCredentials, out TokenResponse tokenResponse))
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