using RivitaBackend.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Services
{
    /// <summary>
    /// Define all methods we will use for Authentication in AuthManager class.
    /// </summary>
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
