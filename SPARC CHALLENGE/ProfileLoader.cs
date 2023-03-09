using Newtonsoft.Json.Linq;
using System;

namespace SPARC_CHALLENGE
{


    public class ProfileLoader
    {
        private string pilot;
        private string mission;
        private string aircraft;    // unique aircraft id, specific to each "aircraft" aka computer
        private LockoutCounter counter;


        public ProfileLoader(string pilot, string mission, string aircraft, LockoutCounter counter) //we can either give it a lockout counter or have it create a lockout counter
        {
            this.pilot = pilot;
            this.mission = mission;
            this.aircraft = aircraft;
            this.counter = counter;
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
                if (objAircraft == profile.aircraft && objName == profile.pilot && objMission == profile.mission)
                {
                    return true;
                }
            }

            // If no match was found, return false
            return false;
        }

        public void Load(Aircraft AC)
        {


            //check that this is the target Aircraft being requested
            if (this.aircraft == AC.ID)
            {   //


                if (this.counter.IsLocked)  //locked out of attempts, not authorized to fly.
                {
                    Console.WriteLine("locked out in class.");
                    return;
                    
                }

                //if not authorized
                if ( !Authorized(this))
                {
                    counter.Increment(); //not authorized so Increment counter
                    Console.WriteLine("not authorized to fly.");                 //notify security officer
                    return;
                    //exit
                }
                else
                {
                    counter.Reset();
                    Console.WriteLine("authorized to fly.");
                    //load profile
                }
            }
            //not for this aircraft so ignore
            return;
        }






    }
}