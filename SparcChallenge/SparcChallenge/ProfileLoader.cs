using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SPARC_CHALLENGE
{


    public class ProfileLoader
    {
        private Pilot pilot;       //create this as a pilot class to reference would hold the lockoutcounter
        private string mission;
        private string aircraft;    // unique aircraft id, specific to each "aircraft" aka computer
  

        
        public ProfileLoader(Pilot pilot, string mission, string aircraft) 
        {
            this.pilot = pilot;
            this.mission = mission;
            this.aircraft = aircraft;

        }

        public static bool Authorized(ProfileLoader profile, Aircraft AC)
        {
            string jsonData;
            string filePath = AC.SystemPaths.AuthorizationFile; 
            // Read Authorization File create it as an excel file? 
            jsonData = FileProcessor.ProcessFile(filePath);
            // Parse the JSON data
            JArray jsonArray = JArray.Parse(jsonData);

            // Iterate over each object in the JSON array
            foreach (JObject obj in jsonArray)
            {
                // Get the values of the aircraft, name, and mission properties
                string objAircraft = obj.Value<string>("aircraft");
                string objName = obj.Value<string>("pilot");
                string objMission = obj.Value<string>("mission");

                // Check if there is a match
                if (objAircraft == profile.aircraft && objName == profile.pilot.pilot && objMission == profile.mission)
                {
                    return true;
                }
            }

            // If no match was found, return false
            return false;
        }

        public void Load(Aircraft AC)
        {
            SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
       

            if (this.pilot.counter.IsLocked)  //locked out of attempts, not authorized to fly.
            {
                if (AC.Leader)
                {
                    Console.WriteLine("Not authorized to fly, locked out.");
                    //notify security officer and user
                    smsMessageTransmitter.SendRejection(this.pilot.pilot);
                }
                
                return;
                    
            }

            //if not authorized
            if ( !Authorized(this,AC))
            {
                    
                if (AC.Leader)
                {
                    //notify security officer
                    //Console.WriteLine("not authorized to fly.");
                    smsMessageTransmitter.SendRejection(this.pilot.pilot);
                } 
                                   

                this.pilot.Increment(); //not authorized so Increment counter
                return;
               
            }
            else
            {
                    
                //if ac == current system then
                if(AC.Leader == true)
                {
                   
                    smsMessageTransmitter.SendConfirmation(this.pilot.pilot);
                }
                    
                //load profile
                if(AC.ID == this.aircraft)
                {
                    
                    //load profile to system aka output data to ACPilotConfig.txt
                    string json = FileProcessor.ProcessFile(AC.SystemPaths.ExternalDriveFile);

                    //find the json obj that matches profile and output to a ACPilotConfig.txt file
                    JArray jsonArray = JArray.Parse(json);

                    // Iterate over each object in the JSON array
                    foreach (JObject obj in jsonArray)
                    {
                        // Get the values of the aircraft, name, and mission properties
                        string objMission = obj.Value<string>("mission");
                        string objPilot = obj.Value<string>("pilot");
                        string objAC = obj.Value<string>("aircraft");
                        // Check if there is a match
                        if (objPilot == this.pilot.pilot && objAC == AC.ID && objMission == this.mission)
                        {
                            //output json object to text 
                            json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                            //output file to drive
                            File.WriteAllText(AC.SystemPaths.IntelligenceModule + "ACPilotConfig.txt", json);
                            return;
                        }
                    }


                }
                this.pilot.Reset();
            }
            
            
            return;
        }






    }
}