using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using BackendApi.Models;
using BackendApi.Services;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        private const string SAP_API_URL = "http://sapapd.railway.ge:8000/sap/zemployee_api";

        public UserController(
            IHttpClientFactory clientFactory, 
            IConfiguration configuration,
            IIdentityService identityService)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            try
            {
                // Get PERNR from Windows identity
                string pernr = _identityService.GetCurrentUserPernr();

                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync($"{SAP_API_URL}?PERNR={pernr}");

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    return Ok(user);
                }
                
                return StatusCode((int)response.StatusCode, "Error fetching user data from SAP");
            }
            catch (Exception ex)
            {
                // For development, you might want to return mock data when the API is not accessible
                #if DEBUG
                var mockUser = new User { 
                    Firstname = "John",
                    Lastname = "Doe",
                    Pernr = "12345"
                };
                return Ok(mockUser);
                #else
                return StatusCode(500, "Internal server error");
                #endif
            }
        }
    }
}