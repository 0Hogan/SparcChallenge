using Newtonsoft.Json.Linq;
using System;

namespace SPARC_CHALLENGE
{


    public class ProfileLoader
    {
        private Pilot pilot;       //create this as a pilot class to reference would hold the lockoutcounter
        private string mission;
        private string aircraft;    // unique aircraft id, specific to each "aircraft" aka computer
  

        
        public ProfileLoader(Pilot pilot, string mission, string aircraft) //we can either give it a lockout counter or have it create a lockout counter
        {
            this.pilot = pilot;
            this.mission = mission;
            this.aircraft = aircraft;

        }

        public static bool Authorized(ProfileLoader profile)
        {
            string jsonData;
            string filePath = "C:/Users/micha/Documents/SPARC/SPARCAuthorization.xlsx";
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


 

            if (this.pilot.counter.IsLocked)  //locked out of attempts, not authorized to fly.
            {
                if (AC.Leader)
                {
                    Console.WriteLine("Not authorized to fly, locked out.");
                    //notify security officer
                }
                
                return;
                    
            }

            //if not authorized
            if ( !Authorized(this))
            {
                    
                if (AC.Leader)
                {
                    //notify security officer
                    Console.WriteLine("not authorized to fly.");
                } 
                                   

                this.pilot.Increment(); //not authorized so Increment counter
                return;
                //exit
            }
            else
            {
                    
                //if ac == current system then
                if(AC.Leader == true)
                {
                    Console.WriteLine("authorized to fly.");
                }
                    
                //load profile
                if(AC.ID == this.aircraft)
                {
                    Console.WriteLine("FLYING");
                    //load profile to system
                }
                this.pilot.Reset();
            }
            
            
            return;
        }






    }
}