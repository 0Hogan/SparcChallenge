﻿using Newtonsoft.Json.Linq;
using System;

using System.IO;


namespace SPARC_CHALLENGE
{
    public class Aircraft
    {
        //have connection to sending notifications?? since the AC is the one that sends the message, before sending it could check if leader if not then wont send.
        internal string ID { get; private set; }
        internal bool Leader { get; private set; }
        internal bool Available { get; private set; }

        public FilePaths SystemPaths;

        public Aircraft()
        {
            
            SystemPaths = new FilePaths();

            //set available based off if the external drive is set up
            if(SystemPaths.ExternalDriveFile == "")
            {
                Available = false;
            }
            else
            {
                Available = true;
            }

            ID = ReadAircraftIdFromFile(SystemPaths.CurrentSystem);
            //add in alphabetical notifier, look into list of ac, if alphabetically first then set leader = true, else set false.

            if(ID == FindLeader(SystemPaths.FleetFile))
            {
                Leader = true;
            }
            else
            {
                Leader = false; 
            }


        }

        private string FindLeader(string FilePath)
        {
            string smallestValue = null;
            //get json data
            string json = FileProcessor.ProcessFile(FilePath);
            //search json data
            //parse json data to object
            //JObject jsonObject = JObject.Parse(json);


            JArray jsonArray = JArray.Parse(json);

            
            //find smallest in array
            foreach (JObject obj in jsonArray)
            {
                string stringValue = obj.Value<string>("A/C Fleet");
                
                //if smallest value is null or current is smaller update
                if(smallestValue == null || stringValue.CompareTo(smallestValue) < 0)
                {
                    smallestValue = stringValue;
                }
            }
            
            //return leader value
            return smallestValue;
        }

        private string ReadAircraftIdFromFile(string filePath)
        {
            // Read the contents of the file
            string fileContent = File.ReadAllText(filePath);

            // Set the ID based on the contents of the file
            string aircraftId = fileContent.Trim();

            return aircraftId;
        }

        private string ReadFleetFromFile(string filePath)
        {
            // Read the contents of the file
            string fileContent = File.ReadAllText(filePath);

            // Set the ID based on the contents of the file
            string aircraftId = fileContent.Trim();

            return aircraftId;
        }
    }

}