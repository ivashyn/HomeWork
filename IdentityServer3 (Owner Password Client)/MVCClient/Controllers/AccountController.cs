using MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityModel;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.Owin.Security.Jwt;
using System.Security.Cryptography.X509Certificates;
using Sample;

namespace MVCClient.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            TokenResponse token = await GetToken(model.Username, model.Password);

            await SignInAsync(token);

            return RedirectToAction("Index", "Home");
        }

        private async Task<TokenResponse> GetToken(string user, string password)
        {
            var client = new TokenClient(
                Constants.TokenEndpoint,
                "ro.client",
                "secret");

            var result = await client.RequestResourceOwnerPasswordAsync(user, password, "read write");
            return result;

        }
        public async Task SignInAsync(TokenResponse token)
        {
            var claims = await ValidateIdentityTokenAsync(token);

            var id = new ClaimsIdentity(claims, "Cookies");
            id.AddClaim(new Claim("access_token", token.AccessToken));
            id.AddClaim(new Claim("expires", DateTime.Now.AddSeconds(token.ExpiresIn).ToLocalTime().ToString()));
            Request.GetOwinContext().Authentication.SignIn(id);
        }

        private async Task<IEnumerable<Claim>> ValidateIdentityTokenAsync(TokenResponse token)
        {
            return await Task.Run<IEnumerable<Claim>>(() =>
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                var cert = Certificate.Get();


                System.IdentityModel.Tokens.TokenValidationParameters validationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = Constants.BaseAddress + "/resources",
                    ValidIssuer = Constants.BaseAddress,
                    NameClaimType = "name",
              
                    IssuerSigningTokens = new X509CertificateSecurityTokenProvider(
                             Constants.BaseAddress,
                             cert).SecurityTokens
                };

                SecurityToken t;
                ClaimsPrincipal id = tokenHandler.ValidateToken(token.AccessToken, validationParameters, out t);
                var claimList = id.Claims.ToList();
                return claimList.AsEnumerable();
            });

        }

        [HttpGet]
        public ActionResult LogOff()
        {
            Request.GetOwinContext()
                .Authentication
                .SignOut("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}