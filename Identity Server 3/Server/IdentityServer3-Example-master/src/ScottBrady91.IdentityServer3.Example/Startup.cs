using IdentityServer3.Core.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Owin;
using ScottBrady91.IdentityServer3.Example;
using ScottBrady91.IdentityServer3.Example.Configuration;

[assembly: OwinStartup("InMemory", typeof(Startup))]

namespace ScottBrady91.IdentityServer3.Example
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map(
                "/core",
                coreApp =>
                {
                    coreApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Standalone Identity Server",
                        SigningCertificate = Cert.Load(),
                        Factory =
                        new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                        RequireSsl = true,
                        AuthenticationOptions = new AuthenticationOptions
                        {
                            EnablePostSignOutAutoRedirect = true,
                            IdentityProviders = ConfigureIdentityProviders
                        }
                    });
                });


        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Sign-in with Google",
                SignInAsAuthenticationType = signInAsType,

                ClientId = "173790617917-munqao8f7c7g2mg25j3shacpd7d64onq.apps.googleusercontent.com",
                ClientSecret = "KuRqHRh-4SKfMGZ52kTqmhcD"
            });

            app.UseFacebookAuthentication(new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions
            {
                AppId = "313935195748960",
                AppSecret = "4b52dda0ce267e5d9a9da5ab80808b66",
                SignInAsAuthenticationType = signInAsType
            });

            app.UseMicrosoftAccountAuthentication(new Microsoft.Owin.Security.MicrosoftAccount.MicrosoftAccountAuthenticationOptions
            {
                ClientId = "4fb7ed8b-5a2b-438b-a8d8-a961582b09f3",
                ClientSecret = "bqWwQ7wsGMaS3nSNJZaHhmp",
                SignInAsAuthenticationType = signInAsType
            });

            app.UseTwitterAuthentication(new Microsoft.Owin.Security.Twitter.TwitterAuthenticationOptions
            {
                ConsumerKey = "HaTvnRxNlzc4m4M5wdDWD9WQ1",
                ConsumerSecret = "usVE3IsFBG3qhFHetRTVZwP7DIhMlhCVdCFz2a9DouJsRlVM4j",
                SignInAsAuthenticationType = signInAsType
            });
        }
    }
}