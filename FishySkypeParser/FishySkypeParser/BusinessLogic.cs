using FishySkypeParser.DataModels;
using FishySkypeParser.DataTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace FishySkypeParser
{
    public class BusinessLogic
    {
        public MainDataModel MainDataModel { get; set; } = null;

        private static readonly string[] _reservedPeers = new string[] { null, "Echo / Sound Test Service" };

        public BusinessLogic()
        {
            MainDataModel = new MainDataModel();

            ReadSettings();
        }

        public void LoadJson()
        {
            MainDataModel.theFile = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }
                .Deserialize<FSPFile>(File.ReadAllText(MainDataModel.JsonFilePath, Encoding.UTF8));

            MainDataModel.theFile.conversations = MainDataModel.theFile.conversations
                .Where(c => (c.MessageList != null) && (c.MessageList.Count() > 0));

            MainDataModel.Peers = MainDataModel.theFile?
                .conversations?
                .Select(c => c.displayName ?? c.id)
                .Distinct()
                .Where(p => !_reservedPeers.Any(rp => rp == p));


            MainDataModel.Myself = MainDataModel.theFile.userId;
        }

        internal void ReadSettings()
        {

        }

        internal void SaveSettings()
        {
            
        }

    }
}
