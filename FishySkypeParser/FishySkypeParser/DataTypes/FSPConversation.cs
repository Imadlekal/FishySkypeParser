using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishySkypeParser.DataTypes
{
    public class FSPConversation
    {
        public string id { get; set; }
        public string displayName { get; set; }
        //public string version { get; set; }
        //public FSPConversationProperties properties { get; set; }
        //public FSPConversationThreadProperties threadProperties { get; set; }
        public IEnumerable<FSPMessage> MessageList { get; set; }
    }
}
