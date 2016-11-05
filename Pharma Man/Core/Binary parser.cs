using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma_Man.Core
{
    public static class Binary_Parser
    {

        public static void SaveTagesplan(Core.Tagesplan tagesplan)
        {
            if (!Directory.Exists("./Tagespläne")) Directory.CreateDirectory("./Tagespläne");
            using (Stream stream = File.Open("./Tagespläne/"+tagesplan.Date.ToShortDateString()+"_Tagesplan.bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, tagesplan);
            }
        }

        public static Core.Tagesplan LoadTagesplan(string path)
        {
            Core.Tagesplan tagesplan;
            try
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    tagesplan = (Core.Tagesplan)bformatter.Deserialize(stream);
                    return tagesplan;
                }
            }
            catch
            {
                return null;
            }
            
        }

        public static void SaveBesuch()
        {

        }

        public static void LoadBeleg()
        {

        }


    }
}
