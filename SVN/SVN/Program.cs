using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Globalization;
using Seisint.LogNamespace;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using RIAG.MBS.WS;
using System;
using System.Text.RegularExpressions;
using WeOnlyDo.Client;

namespace SVN
{

    public class KeyValuePair
    {
        public int key;
        public string description;
    }

    class Customer
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }

    class Order
    {
        public int KeyCode { get; set; }
        public string Product { get; set; }
    }

    class Result
    {
        public string Name { get; set; }
        public IEnumerable<Order> Collection { get; set; }
        public Result(string name, IEnumerable<Order> collection)
        {
            this.Name = name;
            this.Collection = collection;
        }
    }

    public class Dosage
    {
        public int id { get; set; }
        public string name { get; set; }
        public string drug { get; set; }

        public Dosage(int id, string name, string drug)
        {
            this.id = id;
            this.name = name;
            this.drug = drug;
        }

    }

    public class Group
    {
        public string group_code { get; set; }
        public string group_id { get; set; }
        public string parent_group_id { get; set; }

        public Group(string group_code, string group_id, string parent_group_id)
        {
            this.group_code = group_code;
            this.group_id = group_id;
            this.parent_group_id = parent_group_id;
        }
    }

    class Program
    {
        public static List<Group> GroupList = new List<Group>();
        public static List<string> ResultGroupList = new List<string>();

        public static void GetChildGroups(List<string> ChildGroupList)
        {
            foreach(string rcgroup_id in ChildGroupList)
            {
                string group_id = (from grp in GroupList
                                   where grp.group_id == rcgroup_id
                                   select grp.group_id).FirstOrDefault().ToString();

                List<string> groupList = (from grp in GroupList
                                               where grp.parent_group_id == group_id
                                               select grp.group_id).ToList();

                
                foreach (string childGroupid in groupList)
                {
                    ResultGroupList.Add(childGroupid);
                    string cgroup_id = (from grp in GroupList
                                        where grp.group_id == childGroupid
                                       select grp.group_id).FirstOrDefault().ToString();

                    List<string> cGroupList = (from grp in GroupList
                                               where grp.parent_group_id == cgroup_id
                                               select grp.group_id).ToList();

                    if (cGroupList != null && cGroupList.Count > 0)
                    {
                        GetChildGroups(cGroupList);
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            SFTP downloadFTP = null;

            AddressInfo ftpInfo = new AddressInfo();

            ftpInfo.Host = "mbsweb01.br.seisint.com";
            ftpInfo.Username = "ws_dost";
            ftpInfo.Password = "Seisint1";
            ftpInfo.Timeout = "300";
            ftpInfo.License = "8LQW-N7VA-VYDJ-C29F";
            ftpInfo.Blocking = "true";

            FTPConfig.DownloadFTPAddress = ftpInfo;

            try
            {
                downloadFTP = FTPConnection.getFTPConnection();
                downloadFTP.Connect();

                // Create folder if not already exists in IIS server to hold the files from the server for processing.
                if (!Directory.Exists("..\\..\\Documents\\"))
                {
                    Directory.CreateDirectory("..\\..\\Documents\\");
                }

                // Get file from remote server to local path.
                downloadFTP.GetFile("..\\..\\Documents\\", "/u/mbs_uploads/accassi/201603/1457376627.csv");

                downloadFTP.Disconnect();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }




            /*GroupList.Add(new Group("SECURITY1", "1", ""));
            GroupList.Add(new Group("SECURITY", "2", "1"));
            GroupList.Add(new Group("INVWORKBENCH", "3", "1"));
            GroupList.Add(new Group("COMPANY", "4", "2"));
            GroupList.Add(new Group("MISCELLANEOUS", "5", "1"));
            GroupList.Add(new Group("INVWORKBENCH", "6", ""));
            GroupList.Add(new Group("INVWORKBENCH", "7", ""));

            string inputGroup = "SECURITY1";

            ResultGroupList = new List<string>();


            string group_id = (from grp in GroupList
                               where grp.group_code == inputGroup
                               select grp.group_id).FirstOrDefault().ToString();

            ResultGroupList.Add(group_id);

            List<string> ChildGroupList = (from grp in GroupList
                                           where grp.parent_group_id == group_id
                                           select grp.group_id).ToList();

            foreach (string grp in ChildGroupList)
            {
                ResultGroupList.Add(grp);
            }


            GetChildGroups(ChildGroupList);


            foreach (string grp in ResultGroupList)
            {
                Console.WriteLine(grp);
            }*/
        }









            /*DataTable nametable = new DataTable();
            nametable.Columns.Add("id", typeof(int));
            nametable.Columns.Add("name", typeof(string));

            // Here we add five DataRows.
            nametable.Rows.Add(25, "David");
            nametable.Rows.Add(50, "Sam");
            nametable.Rows.Add(10, "Christoff");
            nametable.Rows.Add(21, "Janet");
            nametable.Rows.Add(100, "Melanie");

            List<Dosage> DosageList = new List<Dosage>();
            DosageList.Add(new Dosage(25, "David", "Indocin"));
            DosageList.Add(new Dosage(21, "Janet", "Combivent"));
            /*DosageList.Add(new Dosage(25, "David", "Indocin1"));
            DosageList.Add(new Dosage(100, "Melanie", "Dilantin"));
            DosageList.Add(new Dosage(100, "Melanie", "Dilantin1"));*/


            /*var items = from tb2 in nametable.AsEnumerable()
                        join l1 in DosageList
                        on new
                        {
                            Id = tb2.Field<int>("id")
                        }
                        equals new
                        {
                            Id = l1.id
                        }
                        into grp1
                        select new
                        {
                            ID = tb2.Field<int>("Id"),
                            Name = tb2.Field<string>("name")
                        };

            foreach (var item in items)
            {
                Console.WriteLine(item);

            }*/


            


            

            
            
            

        

        public static string[] Tokenize(string equation)
        {
            Regex RE = new Regex(@"([\+\-\*\(\)\^\\])");
            return (RE.Split(equation));
        }
    }

    /*
    public class student
    {
        public string id { get; set; }
        public string name { get; set; }
        public student()
        { 
        }
        public student(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string ToXML()
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(this.GetType(), string.Empty);

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(string.Empty, string.Empty);

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.OmitXmlDeclaration = true;

            XmlWriter xmlWriter = XmlWriter.Create(stringwriter, xmlWriterSettings);

            serializer.Serialize(xmlWriter, this, xmlSerializerNamespaces);
            return stringwriter.ToString();
        }
    }


    [Serializable]
    public class GroupAttribute : ICloneable
    {
        public bool ReadOnly { get; set; }
        public bool Display { get; set; }

        public GroupAttribute() { }

        public GroupAttribute(bool readOnly, bool display)
        {
            this.ReadOnly = readOnly;
            this.Display = display;
        }

        public object Clone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, this);

            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();

            return obj;
        }
    }*/
}
