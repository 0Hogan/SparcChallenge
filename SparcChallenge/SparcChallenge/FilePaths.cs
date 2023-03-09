using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPARC_CHALLENGE
{
    public class FilePaths
    {
        internal string ExternalDriveFile { get; private set; }
        internal string AuthorizationFile { get; private set; }
        internal string TransientPilotInformation { get; private set; }
        internal string FleetFile { get; private set; }
        internal string CurrentSystem { get; private set; }
        internal string IntelligenceModule { get; private set; }
        public FilePaths()
        {
            ExternalDriveFile = FindExternal(); //find the external drive, this is necessary to then determine if the system is unavailable.

            string Initpath = "ACInitFiles.txt";

            //path for Transient
            string tempTransient = "TransientPilotInformation.csv";
          
            //read first four lines
            string[] lines = File.ReadAllLines(Initpath);
            

            AuthorizationFile = lines[0];
        
            TransientPilotInformation = tempTransient;
            FleetFile = lines[1];
            CurrentSystem = lines[2];
            if (!Directory.Exists(lines[3]))
            {
                Directory.CreateDirectory(lines[3]);
            }
            IntelligenceModule = lines[3];
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
