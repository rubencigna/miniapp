using System.Text.Json;

namespace Services;

public class AuthService
{
    public static async Task<bool> ValidateLogin(string eUsername, string ePassword)
    {
        bool isOK = false;
        using (HttpClient client = new())
        {
            client.BaseAddress = new Uri(IAPIService.BaseAddress);
            var sJson = JsonSerializer.Serialize(new { username = eUsername, password = ePassword });
            var payload = new StringContent(sJson,System.Text.Encoding.UTF8,"application/json");
            var resp = await client.PostAsync(IAPIService.AuthEndpointValidate, payload);
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsStringAsync();
                if (result == "true")
                    isOK = true;
                else
                    isOK = false;
            }
        }
        return isOK;
    }
}