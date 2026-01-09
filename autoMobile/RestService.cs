using Newtonsoft.Json;
using autoMobile.Models;
using System.Text;

namespace autoMobile
{
    public class RestService
    {
        HttpClient client;
        string Url = "https://10.0.2.2:7012/api/Programares/";

        public RestService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            client = new HttpClient(handler);
        }

        public async Task<List<Programare>> GetProgramariByPlateAsync(string plate)
        {
            try
            {
               
                string encodedPlate = Uri.EscapeDataString(plate);
                var response = await client.GetAsync(Url + "ByPlate/" + encodedPlate);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Programare>>(content);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Eroare la conexiune: " + ex.Message);
            }
            return new List<Programare>();
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                
                var response = await client.PostAsync("https://10.0.2.2:7012/api/Account/login", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Eroare Login: " + ex.Message);
                return false;
            }
        }
        public async Task SaveProgramareAsync(Programare item, bool isNewItem)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (isNewItem)
            {
                await client.PostAsync(Url, content);
            }
            else
            {
                await client.PutAsync(Url + item.id, content);
            }
        }

        public async Task DeleteProgramareAsync(int id)
        {
            await client.DeleteAsync(Url + id);
        }
    }
}