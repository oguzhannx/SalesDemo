using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SalesDemo.Helper.Connection
{
    public interface IHttpConnection<T>
    {
        Task<T> GetAsync(string url);
        Task<T> GetAsync(string url, string? headerName, string? value);
        Task<T> PostAsync(string url, T data);
        Task<T> PostAsync(string url, T data, string? headerName, string? value);
        Task<T> PutAsync(string url, T data);
        Task DeleteAsync(string url);
        Task<bool> tokenAsync(string url, T data, HttpContext context);
        Task<bool> register(string url, T data);
    }
}
