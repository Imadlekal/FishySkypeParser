using System;
using System.Collections.Generic;

namespace FishySkypeParser.DataTypes
{
    public class FSPMessage
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public DateTime? originalarrivaltime { get; set; }
        //public string messagetype { get; set; }
        //public string version { get; set; }
        public string content { get; set; }
        public string conversationid { get; set; }
        public string from { get; set; }
        //public FSPMessageProperties properties { get; set; }
        //public IEnumerable<string> amsreferences { get; set; }
    }
}
