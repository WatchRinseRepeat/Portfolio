using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitchOverViewer.Models;

namespace TwitchOverViewer.Controllers
{
    internal class WebServer
    {
        private HttpListener _listener;

        public WebServer (string uri)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(uri);
        }

        public async Task<Authorization?> Listen()
        {
            _listener.Start();
            return await OnRequest();
        }

        private async Task<Authorization?> OnRequest()
        {
            while (_listener.IsListening)
            {
                var ctx = await _listener.GetContextAsync();
                var req = ctx.Request;
                var resp = ctx.Response;

                using (var writer = new StreamWriter(resp.OutputStream))
                {
                    if (req.QueryString.AllKeys.Any("code".Contains))
                    {
                        writer.WriteLine("Authorization Started! Check your application!");
                        writer.Flush();
                        return new Authorization(req.QueryString["code"]);
                    }
                    else
                    {
                        writer.WriteLine("No code found in query string!");
                        writer.Flush();
                    }
                }
            }
            return null;
        }
    }
}
