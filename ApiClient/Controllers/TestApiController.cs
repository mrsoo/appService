using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiClient.Controllers
{
    public class TestApiController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TestApiController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [Route("/callapi")]
        public async Task<IActionResult> callApi()
        {
            //retrieve access token
            var serverClient = httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("http://localhost:3000/");

            var tokenResp = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope="ApiTest",
                GrantType = GrantTypes.Password,
                Parameters =
                {
                    {"username","Email1@gmail.com" },
                    {"password","Abc123" }
                }
            });
            
            //retrieve secret data
            if (tokenResp.IsError)
                return BadRequest(new { message = tokenResp.Json is null?"null":tokenResp.Json.ToString(), statusCode = tokenResp.HttpStatusCode });
            var apiClient = httpClientFactory.CreateClient();
            {
                apiClient.SetBearerToken(tokenResp.AccessToken);
                var resp = await apiClient.GetAsync("http://localhost:3001/identity");
                var content = await resp.Content.ReadAsStringAsync();
                return Ok(new { access_token = tokenResp.AccessToken, message = content });
            }

        }
    }
}
