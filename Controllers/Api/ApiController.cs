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
    public async Task<IActionResult> StoreReading(string sub_domain)
    {
        try
        {
            // Ambil semua query string key-value
            var queryParams = HttpContext.Request.Query;
            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            var targetUrl = $"http://{sub_domain}.higertech.com/api/device/store?{queryString}";

            // Kirim request GET ke subdomain target
            var response = await _httpClient.GetAsync(targetUrl);
            var content = await response.Content.ReadAsStringAsync();

            // Kembalikan response dari server tujuan langsung sebagai JSON
            return Content(content, "application/json", Encoding.UTF8);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}