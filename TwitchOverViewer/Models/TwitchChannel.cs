﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchOverViewer.Models
{
    public class TwitchChannel
    {
        public string? id { get; set; }
        public string? user_id { get; set; }
        public string? user_login { get; set; }
        public string? user_name { get; set; }
        public string? game_id { get; set; }
        public string? game_name { get; set; }
        public string? type { get; set; }
        public string? title { get; set; }
        public int viewer_count { get; set; }
        public string? started_at { get; set; }
        public string? language { get; set; }
        public string? thumbnail_url { get; set; }
        public List<string>? tag_ids { get; set; }
        public List<string>? tags { get; set; }
        public bool is_mature { get; set; }

        TwitchChannel() { }
    }

    public class Root 
    { 
        public List<TwitchChannel> data { get; set; }
    }

}