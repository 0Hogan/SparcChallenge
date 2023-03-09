using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotVerification
{
    public class FilePaths
    {
        internal string ExternalDriveFile { get; private set; }
        internal string AuthorizationFile { get; private set; }
        internal string TransientPilotInformation { get; private set; }
        internal string FleetFile { get; private set; }
        internal string CurrentSystem { get; private set; }

        public FilePaths()
        {
            ExternalDriveFile = FindExternal(); //find the external drive, this is necessary to then determine if the system is unavailable.
            AuthorizationFile = "C:/Users/micha/Documents/SPARC/SPARCAuthorization.xlsx";
            TransientPilotInformation = "C:/Users/micha/Documents/SPARC/TransientPilotInformation.csv";
            FleetFile = "C:/Users/micha/Documents/SPARC/FleetData.xlsx";
            CurrentSystem = "C:/Users/micha/Documents/SPARC/ACID.txt"; 
        }
        


        static string FindExternal()
        {
            string Filename = "ExternalDriveDatabase.xlsx";

            //get all of the drive letters on the system
            DriveInfo[] drives = DriveInfo.GetDrives();

            //loop through each drive

            foreach (DriveInfo drive in drives)
            {
                //check if drive is available
                if (drive.IsReady)
                {
                    //search the root directory of the drive for the file
                    string filepath = Path.Combine(drive.RootDirectory.FullName, Filename);
                    if (File.Exists(filepath))
                    {
                        return filepath;
                    }
                }
            }
            return ""; //path wasn't found
        }

    }
}
