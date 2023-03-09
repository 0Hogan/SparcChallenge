using System;

using System.IO;


namespace SPARC_CHALLENGE
{
    public class Aircraft
    {
        internal string ID { get; private set; }

        public Aircraft(string filePath)
        {
            ID = ReadAircraftIdFromFile(filePath);
        }

        private string ReadAircraftIdFromFile(string filePath)
        {
            // Read the contents of the file
            string fileContent = File.ReadAllText(filePath);

            // Set the ID based on the contents of the file
            string aircraftId = fileContent.Trim();

            return aircraftId;
        }
    }
}