using Microsoft.AspNetCore.Mvc.Testing;
using GeocacheSolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;

namespace GeocacheTests
{
    internal class GeocacheWebApplicationFactory : WebApplicationFactory<Program>
    {
        public IConfiguration Configuration { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config => 
            {
                Configuration = new ConfigurationBuilder()
                .AddJsonFile("integrationsettings.json")
                .Build();

                config.AddConfiguration(Configuration);
            });
            
            builder.ConfigureTestServices(services => 
            {
                services
                .AddAuthentication("IntegrationTest")
                .AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>(
                    "IntegrationTest",
                    options => { }
                );
                services.AddTransient<IGeocacheConfigService, GeocacheConfigStub>();
            });
        }
        internal class IntegrationTestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            public IntegrationTestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
              ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
              : base(options, logger, encoder, clock)
            {
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                var claims = new[] {
            new Claim(ClaimTypes.Name, "IntegrationTest User"),
            new Claim(ClaimTypes.NameIdentifier, "IntegrationTest User"),
            new Claim("a-custom-claim", "squirrel 🐿️"),
        };
                var identity = new ClaimsIdentity(claims, "IntegrationTest");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "IntegrationTest");
                var result = AuthenticateResult.Success(ticket);
                return Task.FromResult(result);
            }
        }
    }
}
