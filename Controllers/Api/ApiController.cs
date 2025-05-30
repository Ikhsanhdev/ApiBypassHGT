using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[ApiController]
public class ApiController : ControllerBase {
    private readonly HttpClient _httpClient;

    public ApiController(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    [HttpGet("{sub_domain}/api/device/store")]
    public async Task<IActionResult> StoreReading(string sub_domain) {
        try {
            var queryParams = HttpContext.Request.QueryString.Value;

            // Bypass ke subdomain
            var targetUrl = $"http://{sub_domain}.higertech.com/api/device/store{queryParams}";

            var response = await _httpClient.GetAsync(targetUrl);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(HttpContext.Request.Path);
            Console.WriteLine(targetUrl);

            return Content(content, "application/json");
        } catch (Exception ex) {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}