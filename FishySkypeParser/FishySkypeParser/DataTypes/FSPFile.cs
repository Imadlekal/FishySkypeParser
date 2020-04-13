using System;
using System.Collections.Generic;

namespace FishySkypeParser.DataTypes
{
    public class FSPFile
    {
        public string userId { get; set; }
        //public DateTime? exportDate { get; set; }
        public IEnumerable<FSPConversation> conversations { get; set; }
    }
}
