using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BetterBeer.Services
{
    public interface IFacebookLogin
    {
        Task<FbAccessToken> LogIn(string[] permissions);
        bool IsLoggedIn();
        FbAccessToken GetAccessToken();
        void Logout();
    }
}
