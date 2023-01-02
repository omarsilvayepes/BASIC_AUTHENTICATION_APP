using BasicSecurityASP.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace BasicSecurityASP.Security
{
    public class BasicAuthHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService userService;

        public BasicAuthHandler(IUserService userService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock

            ):base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Header does not exist");

            bool result = false;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var email = credentials[0];
                var pass = credentials[1];
                result = userService.IsUser(email, pass);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Something was wrong");
            }

            if (!result)
                return AuthenticateResult.Fail("User or pass invalid");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,"Id"),
                new Claim(ClaimTypes.Name,"User")
            };

            var identity=new ClaimsIdentity(claims, Scheme.Name);
            var principal=new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal,Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
