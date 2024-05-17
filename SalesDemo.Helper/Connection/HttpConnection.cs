using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SalesDemo.Helper.Connection
{
    public class HttpConnection<T> : IHttpConnection<T>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _handler;

        public HttpConnection()
        {
            _handler = new HttpClientHandler();
            _handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(_handler);





        }

        public async Task<T> GetAsync(string url, string? headerName, string? value)
        {

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (HttpClient client = new HttpClient(handler))
                {
                    if (client.DefaultRequestHeaders.Contains(headerName))
                    {
                        client.DefaultRequestHeaders.Remove(headerName);
                    }
                    client.DefaultRequestHeaders.Add(headerName, value);


                    var response = await client.GetAsync(url);
                    //response.EnsureSuccessStatusCode();
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseData); ;

                }

            }




        }
        public async Task<T> PostAsync(string url, T data, string? headerName, string? value)
        {
        
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (HttpClient client = new HttpClient(handler))
                {


                    if (headerName != null && value != null)
                    {
                        if (client.DefaultRequestHeaders.Contains(headerName))
                        {
                            client.DefaultRequestHeaders.Remove(headerName);
                        }
                        client.DefaultRequestHeaders.Add(headerName, value);
                    }



                    // Veriyi JSON formatına çevirme
                    string jsonData = JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseData);
                }

            }

        }
        public async Task<T> PostAsync(string url, T data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    // Veriyi JSON formatına çevirme
                    string jsonData = JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(responseData);
                }

            }
        }
        public async Task<T> PutAsync(string url, T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData);
        }
        public async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        public Task<T> GetAsync(string url)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> tokenAsync(string url, T data, HttpContext context)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (HttpClient client = new HttpClient(handler))
                {

                    // Veriyi JSON formatına çevirme
                    string jsonData = JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();

                        // JWT'yi bir cookie'ye yerleştirme
                        context.Response.Cookies.Append("jwt", token, new CookieOptions
                        {
                            HttpOnly = true, // Cookie'ye JavaScript erişimini engeller
                            Secure = true,   // Sadece HTTPS üzerinden iletilir
                            SameSite = SameSiteMode.Strict, // CSRF saldırılarını önlemek için güçlendirilmiş güvenlik
                            Expires = DateTime.UtcNow.AddDays(1) // Cookie'nin son kullanma tarihi 
                        });

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }




        }




        public async Task<bool> register(string url, T data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    // Veriyi JSON formatına çevirme
                    string jsonData = JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }



    }
}