using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using PublicAPI.Data;
using PublicAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PublicAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UsersContext _context;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UsersContext context)
            : base(options, logger, encoder, clock)
        {
            _context = context;

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header was not found");

            try
            {
                var authencationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authencationHeaderValue.Parameter);
                string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
                string s_username = credentials[0];
                string s_password = credentials[1];

                Users user = _context.api_users.Where(user => user.username == s_username && user.password == s_password).FirstOrDefault();
                if (user == null)
                    AuthenticateResult.Fail("Invalid Username");
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid Key");
            }

            return AuthenticateResult.Fail("Need to implement");
        }

    }
}
