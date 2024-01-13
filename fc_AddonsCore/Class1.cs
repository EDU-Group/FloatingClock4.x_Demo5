
using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

namespace fc_AddonsCore
{
    public class Addons
    {
        static string Pach = AppDomain.CurrentDomain.BaseDirectory;

        public static string CatchAddon(string AddonName)
        {
            DirectoryInfo di = new DirectoryInfo(Pach + @"\AddonCache\" + AddonName);
            di.Create();
            Directory.CreateDirectory(Pach + @"\AddonCache\" + AddonName);
            ZipFile.ExtractToDirectory(Pach + @"\Imput\" + AddonName + ".fcaddon", Pach + @"\AddonCache\" + AddonName);
            return "d";


        }
    }

}
