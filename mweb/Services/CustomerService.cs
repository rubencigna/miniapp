using System.Text;
using System.Text.Json;
using Models;

namespace Services;

public class CustomerService
{
    public static async Task<bool> Create(CustomerModel data)
    {
        bool result = false;
        using (HttpClient client = new())
        {
            client.BaseAddress=new Uri(IAPIService.BaseAddress);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(IAPIService.CustomerEndpointCreate, content);
            if (resp.IsSuccessStatusCode)
            {
                var res = await resp.Content.ReadFromJsonAsync<CustomerModel>();
                if (res != null)
                    result = true;
            }
        }
        return result;
    }
    public static async Task<List<CustomerModel>> Read(CustomerModel data)
    {
        var result = new List<CustomerModel>();
        using (HttpClient client = new())
        {
            client.BaseAddress=new Uri(IAPIService.BaseAddress);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(IAPIService.CustomerEndpointGetAll, content);
            if (resp.IsSuccessStatusCode)
            {
                var customers = await resp.Content.ReadFromJsonAsync<CustomerModel[]>();
                foreach (var item in customers!)
                {
                    result.Add(new CustomerModel { Email = item.Email, Name = item.Name, Phone = item.Phone });
                }
            }
        }
        return result;
    }
    public static bool Update(object data)
    {
        bool result = false;
        return result;
    }
    public static bool Delete(object data)
    {
        bool result = false;

        return result;
    }




}