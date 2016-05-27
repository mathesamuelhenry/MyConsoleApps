using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FolderSync
{
    public class Config
    {
        public static List<FolderConfig> FolderSyncList { get; set; }
        public static string LogFile { get; set; }
        public string ConfigFile {get; set;}

        public Config(string configFile)
        {
            this.ConfigFile = configFile;
            FolderSyncList = new List<FolderConfig>();
        }

        public void LoadConfig()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.ConfigFile);
            string xmlcontents = doc.InnerXml;

            XmlNodeList nodelist = doc.SelectNodes("/Folders/FolderSync");
            int type = 0;

            if(nodelist != null)
            {
                for (int i = 0; i < nodelist.Count; i++)
                {
                    FolderConfig conf = new FolderConfig();
                    List<string> BiDirectionalList = new List<string>();
                    List<string> DestinationList = new List<string>();

                    XmlNodeList childNodeList = nodelist.Item(i).ChildNodes;
                    type = int.Parse(nodelist[i].Attributes["type"].Value.ToString());
                    conf.Type = type;

                    for (int k = 0; k < childNodeList.Count; k++)
                    {
                        if (childNodeList.Item(k).Name.Contains("Folder"))
                        {
                            BiDirectionalList.Add(childNodeList.Item(k).InnerText.Trim());
                        }
                        else if (childNodeList.Item(k).Name.Contains("Source"))
                        {
                            conf.Source = childNodeList.Item(k).InnerText.Trim();
                        }
                        else if (childNodeList.Item(k).Name.Contains("Destination"))
                        {
                            DestinationList.Add(childNodeList.Item(k).InnerText.Trim());
                        }
                    }
                    conf.BiDirectionalFolders = BiDirectionalList;
                    conf.Destination = DestinationList;

                    FolderSyncList.Add(conf);
                }
            }

            XmlNode logNode = doc.SelectSingleNode("/Folders/LogFile");
            LogFile = logNode.InnerText;
        }

        
    }

    public class FolderConfig
    {
        public List<string> BiDirectionalFolders { get; set; }
        public int Type { get; set; }
        public string Source { get; set; }
        public List<string> Destination { get; set; }
    }

    public class DestinationFiles
    {
        public string Path { get; set; }
        public List<string> DestinationPathFiles { get; set; }
    }

    public partial class Fold
    {
        public int id { get; set; }
    }

    public partial class Fold
    {
        public string name { get; set; }

        public Fold(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
