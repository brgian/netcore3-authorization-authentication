using NetCore.Template.DTOs.Requests;
using NetCore.Template.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Template.Services
{
    public interface IAuthenticationService
    {
        bool TryAuthenticate(LoginCredentials loginCredentials, out TokenResponse tokenResponse);
    }
}
