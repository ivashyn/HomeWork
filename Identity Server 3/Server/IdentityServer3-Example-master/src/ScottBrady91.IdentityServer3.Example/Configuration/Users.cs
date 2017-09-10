﻿using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace ScottBrady91.IdentityServer3.Example.Configuration
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "Vadym",
                    Password = "123456",
                    Subject = "1",
                    Claims = new List<Claim>
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Vadym"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Ivashyn"),
                        new Claim(Constants.ClaimTypes.Email, "ivashyn.vadym@gmail.com"),
                        new Claim(Constants.ClaimTypes.Role, "Admin")
                    }
                }
            };
        }
    }
}