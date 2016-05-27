using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSync
{
    class RunAnyFolderSync
    {
        public static FolderSync RunSync = null;
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new Exception("No config file set");

            RunSync = new FolderSync(args[0]);
            RunSync.Sync();
        }
    }
}
