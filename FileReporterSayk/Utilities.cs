using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReporterSayk
{
    internal class Utilities
    {

        public static Dictionary<string, PathStats> Statistics = new Dictionary<string, PathStats>();

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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\t" + path);
            }
            PathStats currentPathStats = new PathStats(currentPathSize, currentPathItems);
            Statistics.Add(path, currentPathStats);
            Console.WriteLine(path + "\t" + Utilities.BytesToSize(currentPathStats.TotalSize) +"\t" +currentPathStats.NumberItems + " Items ");

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
    }    
}
