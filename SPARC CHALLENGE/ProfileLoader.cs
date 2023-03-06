using Newtonsoft.Json.Linq;
using System;

namespace SparcChallenge
{
    

    public class ProfileLoader
    {
        private string pilot;
        private string mission;
        private string aircraft;
        private LockoutCounter counter;
        
    
        public ProfileLoader(string pilot, string mission, string aircraft, LockoutCounter counter) //we can either give it a lockout counter or have it create a lockout counter
        {
            this.pilot = pilot;
            this.mission = mission;
            this.aircraft = aircraft;
            this.counter = counter;
        }
    
        public void Load(Aircraft AC)
        {
            
            
            //check that this is the target Aircraft being requested
            if(aircraft == AC.ID){   //
                
                
                if(this.counter.IsLocked) {
                   Console.WriteLine("locked out in class.");
                   return;
                   //exit
                }
               
                //if not authorized
                if(true){
                   counter.Increment(); //not authorized so Increment counter
                   return;
                   //exit
                }else{
                   counter.Reset();
                   
                   //load profile
                }
            }
            
            return;
        }
        
        
        
        
        

    
        public static bool Authorized(string jsonData, string aircraft, string name, string mission)
        {
            
            // Read Authorization File
            
            // Parse the JSON data
            JArray jsonArray = JArray.Parse(jsonData);
        
            // Iterate over each object in the JSON array
            foreach (JObject obj in jsonArray)
            {
                // Get the values of the aircraft, name, and mission properties
                string objAircraft = obj.Value<string>("aircraft");
                string objName = obj.Value<string>("name");
                string objMission = obj.Value<string>("mission");
        
                // Check if there is a match
                if (objAircraft == aircraft && objName == name && objMission == mission)
                {
                    return true;
                }
            }
        
            // If no match was found, return false
            return false;
        }

    }
}