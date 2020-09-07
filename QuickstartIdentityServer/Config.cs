// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuickstartIdentityServer
{
	public class Config
	{
		// scopes define the resources in your system
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("buyingAgentAPI", "Buying Agent BackEnd API"),
				new ApiResource("api", "test api")
				//new ApiResource("profile", "profile"),
				//new ApiResource("openid", "openid")
			};
		}

		// clients want to access resources (aka scopes)
		public static IEnumerable<Client> GetClients()
		{
			// client credentials client
			return new List<Client>
			{
				new Client
				{
					ClientId = "buyingAgent client",
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = { "api" }
				},

                // resource owner password grant client
                new Client
				{
					ClientId = "buyingAgent.client",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					AllowedScopes = { "api" },
					AllowOfflineAccess = true
				},

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
				{
					ClientId = "mvc client",
					ClientName = "MVC Client",
					AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					RedirectUris = new List<string>(new string[] { "http://localhost:8002/signin-oidc", "http://192.168.1.5:8002/signin-oidc"}),
					PostLogoutRedirectUris = new List<string>(new string[] { "http://localhost:8002/signout-callback-oidcc", "http://192.168.1.5:8002/signout-callback-oidcc"}),
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api1"
					},
					AllowOfflineAccess = true
				},

				//authorization code 
				new Client
				{
					ClientId = "buyingAgent native",
					ClientName = "Buying Agent Native Client",

					RedirectUris = new List<string>(new string[] { "https://www.getpostman.com/oauth2/callback",
						"http://localhost:8002", "http://192.168.1.5:8002","app.buyingagent:/oauthredirect"}),
					PostLogoutRedirectUris =  new List<string>(new string[] { "https://www.getpostman.com/oauth2/callback",
						"http://localhost:8002", "http://192.168.1.5:8002","app.buyingagent:/oauthredirect"}),

					ClientSecrets = { new Secret("secret".Sha256()) },

					RequireClientSecret = false,

					AllowedGrantTypes = GrantTypes.Code,
					  AllowedScopes = {
						"buyingAgentAPI",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.OfflineAccess
					},

					AccessTokenLifetime = 2100000,
					AllowOfflineAccess = true
				}
			};
		}

		public static List<TestUser> GetUsers()
		{
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "1",
					Username = "BuyingAgent",
					Password = "admin",

					Claims = new List<Claim>
					{
						new Claim("name", "BuyingAgent"),
						new Claim("website", "https://app.buyingagentapp.com")
					}
				},
				new TestUser
				{
					SubjectId = "2",
					Username = "Lucas",
					Password = "admin",

					Claims = new List<Claim>
					{
						new Claim("name", "Lucas"),
						new Claim("website", "https://app.buyingagentapp.com")
					}
				}
			};
		}
	}
}