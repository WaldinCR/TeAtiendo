using TeAtiendo.Web.Models;

namespace TeAtiendo.Web.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string url);
        Task<T?> PostAsync<T>(string url, object data);
        Task<T?> PutAsync<T>(string url, object data);
        Task<bool> DeleteAsync(string url);
        Task<T?> PatchAsync<T>(string url, object data);
        void SetToken(string token);


        Task<ApiResult<T>> GetWithResultAsync<T>(string url);
        Task<ApiResult<T>> PostWithResultAsync<T>(string url, object data);
        Task<ApiResult<T>> PutWithResultAsync<T>(string url, object data);
        Task<ApiResult<T>> PatchWithResultAsync<T>(string url, object data);
        Task<ApiResult<bool>> DeleteWithResultAsync(string url);
    }
}