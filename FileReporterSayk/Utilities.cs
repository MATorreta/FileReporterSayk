using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterSayk
{
    public static class Utilities
    {
        public static string selectedDrivetxt;
        public static Dictionary<string, PathStats> Statistics = new Dictionary<string, PathStats>();
        public static Dictionary<string, PathStats> Extensions = new Dictionary<string, PathStats>();
        public static Dictionary<string, PathStats> ForAddSizes = new Dictionary<string, PathStats>();

        public static void GetFileInfo(string path)
        {
            long currentPathSize = 0;
            int currentPathItems = 0;


            DirectoryInfo di = new DirectoryInfo(path);
            try
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    currentPathSize += file.Length;
                    currentPathItems++;
                    if (!Extensions.ContainsKey(file.Extension))
                    {
                        Extensions.Add(file.Extension, new PathStats(file.Length, 1));
                    }
                    else
                    {
                        Extensions[file.Extension].TotalSize += file.Length;
                        Extensions[file.Extension].NumberItems++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\t" + path);
            }
            PathStats currentPathStats = new PathStats(currentPathSize, currentPathItems);
            Statistics.Add(path, currentPathStats);
            Console.WriteLine(path + "\t" + Utilities.BytesToSize(currentPathStats.TotalSize) + "\t" + currentPathStats.NumberItems + " Items ");

        }
        public static void GetSubFoldersInfo(string path)
        {
            DirectoryInfo selectedDi = new DirectoryInfo(path);
            DirectoryInfo[] folders = selectedDi.GetDirectories();
            foreach (DirectoryInfo folder in folders)
            {
                try
                {
                    Utilities.GetFileInfo(folder.FullName);
                    DirectoryInfo currentFolder = new DirectoryInfo(folder.FullName);
                    GetSubFoldersInfo(folder.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\t" + folders);
                }
            }
        }
        public static string BytesToSize(long bytes)                  //Function for convert Bytes on GB, MB , KB. 
        {
            if (bytes >= 1073741824)
            {
                return (bytes / 1073741824).ToString() + " GB ";
            }
            else if (bytes >= 1048576)
            {
                return (bytes / 1048576).ToString() + " MG ";
            }
            else if (bytes >= 1024)
            {
                return (bytes / 1024).ToString() + " KB ";
            }
            else
                return bytes + "Bytes";
        }
        public static void GetSizeFolders()
        {
            foreach (var item in Utilities.Statistics)
            {
                Console.WriteLine("--------------");
                string path = item.Key;
                string[] fragments = path.Split(@"\");
                string currentFragment = "";
                foreach (string fragment in fragments)
                {
                    currentFragment = currentFragment + fragment + @"\";
                    Console.WriteLine(currentFragment);
                    if (Utilities.ForAddSizes.ContainsKey(currentFragment))
                    {
                        Utilities.ForAddSizes[currentFragment].TotalSize += item.Value.TotalSize;
                        Utilities.ForAddSizes[currentFragment].NumberItems += item.Value.NumberItems;
                    }
                    else
                    {
                        Utilities.ForAddSizes.Add(currentFragment, item.Value);

                    }
                }
            }
        }

        public static void Txt()
        {
            string filePath = "FileReport"+selectedDrivetxt+".tsv";
            WriteToFile(filePath);
        }

        static void WriteToFile(string filePath)
        {
            // Write to the file
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("Path\tSize\tItems");
                foreach (var items in Utilities.ForAddSizes)
                {
                    sw.WriteLine(items.Key + "\t" + Utilities.BytesToSize(items.Value.TotalSize)+"\t"+items.Value.NumberItems);
                }
            }
        }
    }
}


