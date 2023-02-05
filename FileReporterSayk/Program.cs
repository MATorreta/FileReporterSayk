using System.IO;
using System.Net.NetworkInformation;

namespace FileReporterSayk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List of Drives");
            DriveInfo[] drives = DriveInfo.GetDrives();                                                        //use library sistem.IO  for use "get Drives",save in array all drives.
            for (int i = 0; i < drives.Length; i++)                                                             //iterate array  
            {
                Console.WriteLine($"{i + 1}.{drives[i].Name} - {Utilities.BytesToSize(drives[i].TotalSize)}");   // Utilities.BytesToSize:call function BytesToSize created in Utilities.
            }
            int selected;
            bool error = false;
            do
            {
                Console.WriteLine("Choose Drive for analyze");
                error = int.TryParse(Console.ReadLine(), out selected);
                if (selected > drives.Length || selected <= 0 || !error)
                {
                    Console.WriteLine("Invalid entry. Try again.");
                }
            } while (selected > drives.Length || selected <= 0 || !error);

            DriveInfo selectedDrive = drives[selected - 1];                                                  // -1 because Array start in 0, but the list start in 1.        
            Console.WriteLine("Drive selected: " + selectedDrive);
            Utilities.GetFileInfo(selectedDrive.Name);                                                       //Call function in Utilities.GetFileInfo for selected drive.
            Utilities.GetSubFoldersInfo(selectedDrive.Name);
            Console.WriteLine(Utilities.Extensions);    
        }
    }
}