using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KratosDemo.Server.Kratos
{
    public class KratosAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class KratosAuthenticationHandler : AuthenticationHandler<KratosAuthenticationOptions>
    {        
        readonly KratosService _kratosService;

        public KratosAuthenticationHandler(
            IOptionsMonitor<KratosAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            KratosService kratosService
        ) 
            : base(options, logger, encoder, clock)
        {
            _kratosService = kratosService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                // Check, if Cookie was set
                if (Request.Cookies.ContainsKey("ory_kratos_session"))
                {
                    var cookie = Request.Cookies["ory_kratos_session"];
                    var id = await _kratosService.GetUserIdByCookie("ory_kratos_session", cookie);
                    Console.WriteLine("Success:" + id);
                    return ValidateToken(id);
                }

                // Check, if Authorization header was set
                if (Request.Headers.ContainsKey("Authorization"))
                {
                    var token = Request.Headers["Authorization"];
                    var id = await _kratosService.GetUserIdByToken(token);
                    Console.WriteLine("Success:" + id);
                    return ValidateToken(id);
                }

                return AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private AuthenticateResult ValidateToken(string userId)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            };
 
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        } 
    }
}
