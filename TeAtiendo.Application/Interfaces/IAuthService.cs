using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeAtiendo.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);

        Task<bool> RegisterAsync(string nombre, string email, string password);

        Task LogoutAsync();
    }
}