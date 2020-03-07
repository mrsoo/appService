using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EMSystem.Domain.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static IdentityModel.OidcConstants;

namespace EMSystem.Service.Controllers
{
    [Route("api/[controller]")]

    public class EmpController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EmpController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //retrieve access token
            var serverClient = httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("http://localhost:3000/");

            var tokenResp = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "ApiResx",
                GrantType = GrantTypes.Password,
                Parameters =
                {
                    {"username","aaa@gmail.com" },
                    {"password","123abc" }
                }
            });

            //retrieve secret data
            if (tokenResp.IsError)
                return BadRequest(new { message = tokenResp.Json is null ? "null" : tokenResp.Json.ToString(), statusCode = tokenResp.HttpStatusCode });
            var apiClient = httpClientFactory.CreateClient();
            {
                apiClient.SetBearerToken(tokenResp.AccessToken);
                var resp = await apiClient.GetAsync("http://localhost:8080/api/emp");

                return Ok(resp.Content.ReadAsStringAsync().Result);

            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Employee model)
        {
            //retrieve access token
            var serverClient = httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("http://localhost:3000/");

            var tokenResp = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "ApiResx",
                GrantType = GrantTypes.Password,
                Parameters =
                {
                    {"username","aaa@gmail.com" },
                    {"password","123abc" }
                }
            });

            //retrieve secret data
            if (tokenResp.IsError)
                return BadRequest(new { message = tokenResp.Json is null ? "null" : tokenResp.Json.ToString(), statusCode = tokenResp.HttpStatusCode });
            var apiClient = httpClientFactory.CreateClient();
            {
                apiClient.SetBearerToken(tokenResp.AccessToken);
                var resp = await apiClient.PostAsync("http://localhost:8080/api/emp", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                return Ok(resp.Content.ReadAsStringAsync().Result);

            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
