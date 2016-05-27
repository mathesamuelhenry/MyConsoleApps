using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeOnlyDo.Client;

namespace SVN
{
    public class FTPConnection
    {
        public static SFTP getFTPConnection()
        {
            SFTP downloadFTP = new SFTP();
            downloadFTP.LicenseKey = FTPConfig.DownloadFTPAddress.License;
            downloadFTP.Hostname = FTPConfig.DownloadFTPAddress.Host;
            downloadFTP.Login = FTPConfig.DownloadFTPAddress.Username;
            downloadFTP.Password = FTPConfig.DownloadFTPAddress.Password;
            downloadFTP.Blocking = bool.Parse(FTPConfig.DownloadFTPAddress.Blocking);
            downloadFTP.Timeout = short.Parse(FTPConfig.DownloadFTPAddress.Timeout);
            return downloadFTP;
        }
    }
}
