using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace FolderSync
{
    public class FolderSync
    {
        public static System.IO.StreamWriter file { get; set; }

        public FolderSync(string configFile)
        {
            Config config = new Config(configFile);
            config.LoadConfig();

            if (string.IsNullOrEmpty(Config.LogFile))
                throw new Exception("Log file not set");

            file = File.AppendText(Config.LogFile);
        }

        public T GetValue<T> (T value)
        {
            return value;
        }

        public void Sync()
        {
            string k = this.GetValue<string>("herllo");
            int l = this.GetValue<int>(1213);


            /*try
            {
                if (Config.FolderSyncList != null && Config.FolderSyncList.Count > 0)
                {
                    foreach (FolderConfig folderConfig in Config.FolderSyncList)
                    {
                        if (folderConfig.BiDirectionalFolders != null && folderConfig.BiDirectionalFolders.Count > 1)
                        {
                            // source -> Destination
                            List<string> SourcePathList = Directory.GetFiles(folderConfig.BiDirectionalFolders[0].ToString()).ToList();
                            List<string> DestinationPathList = Directory.GetFiles(folderConfig.BiDirectionalFolders[1].ToString()).ToList();
                            string SourcePath = folderConfig.BiDirectionalFolders[0].ToString();
                            string DestinationPath = folderConfig.BiDirectionalFolders[1].ToString();

                            List<string> SourceList = new List<string>();
                            List<string> DestinationList = new List<string>();

                            foreach (string sourceFile in SourcePathList)
                            {
                                SourceList.Add(Path.GetFileName(sourceFile));
                            }

                            foreach (string destinationFile in DestinationPathList)
                            {
                                DestinationList.Add(Path.GetFileName(destinationFile));
                            }

                            foreach (string sourceFile in SourceList)
                            {
                                try
                                {
                                    if (DestinationList.Contains(sourceFile))
                                    {
                                        if (File.GetLastWriteTime(Path.Combine(SourcePath, sourceFile)) !=
                                            File.GetLastWriteTime(Path.Combine(DestinationPath, sourceFile)) &&
                                            File.GetLastWriteTime(Path.Combine(SourcePath, sourceFile)) >
                                            File.GetLastWriteTime(Path.Combine(DestinationPath, sourceFile)))
                                        {
                                            File.Copy(Path.Combine(SourcePath, sourceFile),
                                            Path.Combine(DestinationPath, sourceFile), true);
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(Path.Combine(SourcePath, sourceFile)))
                                        {
                                            Console.WriteLine(SourcePath);
                                            Console.WriteLine(DestinationPath);
                                        }

                                        File.Copy(Path.Combine(SourcePath, sourceFile),
                                            Path.Combine(DestinationPath, sourceFile));
                                    }
                                }
                                catch (Exception e)
                                {

                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(folderConfig.Source) && folderConfig.Destination != null && folderConfig.Destination.Count > 0)
                        {
                            List<string> SourcePathList = Directory.GetFiles(folderConfig.Source).ToList();

                            List<string> SourceList = new List<string>();

                            foreach (string sourceFile in SourcePathList)
                            {
                                SourceList.Add(Path.GetFileName(sourceFile));
                            }

                            foreach (string path in folderConfig.Destination)
                            {
                                //FolderSync.WriteLogEntry(string.Format("Syncing Source folder [{0}] to [{1}]", folderConfig.Source, path));

                                DestinationFiles ds = new DestinationFiles();
                                ds.Path = path;

                                List<string> DestinationList = new List<string>();
                                List<string> DestinationPathList = Directory.GetFiles(ds.Path).ToList();
                                foreach (string destinationFile in DestinationPathList)
                                {
                                    DestinationList.Add(Path.GetFileName(destinationFile));
                                }

                                ds.DestinationPathFiles = DestinationList;

                                foreach (string sourceFile in SourceList)
                                {
                                    try
                                    {
                                        if (ds.DestinationPathFiles.Contains(sourceFile))
                                        {
                                            if (File.GetLastWriteTime(Path.Combine(folderConfig.Source, sourceFile)) !=
                                                File.GetLastWriteTime(Path.Combine(ds.Path, sourceFile)) &&
                                                File.GetLastWriteTime(Path.Combine(folderConfig.Source, sourceFile)) >
                                                File.GetLastWriteTime(Path.Combine(ds.Path, sourceFile)))
                                            {
                                                File.Copy(Path.Combine(folderConfig.Source, sourceFile),
                                                    Path.Combine(ds.Path, sourceFile), true);

                                                FolderSync.WriteLogEntry(string.Format("File changed : {0}", sourceFile));
                                            }
                                        }
                                        else
                                        {
                                            if (File.Exists(Path.Combine(folderConfig.Source, sourceFile)))
                                            {
                                                Console.WriteLine(folderConfig.Source);
                                                Console.WriteLine(ds.Path);
                                            }

                                            File.Copy(Path.Combine(folderConfig.Source, sourceFile),
                                                Path.Combine(ds.Path, sourceFile));

                                            FolderSync.WriteLogEntry(string.Format("File added : {0}", sourceFile));
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                file.Close();
                file.Dispose();
            }*/
        }

        private static void WriteLogEntry(string text)
        {
            file.WriteLine("{0} : {1}", DateTime.Now, text);
        }

        private static void WriteBreak()
        {
            file.WriteLine("-------------------------------------------------------------------------------------------------------");
        }
    }
}
