using Microsoft.AspNetCore.Mvc;

namespace Hgtrg.Ecommerce.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private const string BASE_URL = "https://localhost:44382/api/";

        public IActionResult Register()
        {
            return View();
        }
        public async Task<ActionResult> Login()
        {
            string apiUrl = BASE_URL + "Authenticate/login";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    // Do something with the response content
                }
                else
                {
                    // Handle the error response
                }
            }

            return View();
        }
    }
}
