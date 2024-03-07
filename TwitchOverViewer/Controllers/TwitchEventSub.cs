using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchLib.EventSub.Websockets.Client;

using TwitchOverViewer.ContentPages;

using CommunityToolkit.Mvvm.Messaging;

namespace TwitchOverViewer.Controllers
{
    internal class TwitchEventSub
    {
        private readonly TwitchClient _client;

        public TwitchEventSub()
        {

        }
    }
}
