using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVN
{
    public static class FTPConfig
    {
        public static AddressInfo DownloadFTPAddress { get; set; }
    }
    public class AddressInfo
    {
        public string Blocking { get; set; }
        public string Host { get; set; }
        public string License { get; set; }
        public string Password { get; set; }
        public string Timeout { get; set; }
        public string Username { get; set; }
        public string Domain { get; set; }
    }
}
