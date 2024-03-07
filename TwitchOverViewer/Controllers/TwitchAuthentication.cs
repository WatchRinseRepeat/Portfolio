using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchOverViewer.Controllers
{
    public class TwitchAuthentication
    {
        
    }

    public class Config
    {
        public string UserToken { get; set; }
        public string OAuthToken { get; set; }
        public string ClientId { get; set; }
        public string UserRefreshToken { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
    }

    public class UserConfig
    {
        public string UserToken { get; set;}
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
    }

    public class Authorization
    {
        public string Code { get; }

        public Authorization(string code)
        {
            Code = code;
        }
    }

    
}
