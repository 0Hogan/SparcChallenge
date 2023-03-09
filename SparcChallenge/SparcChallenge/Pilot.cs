using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPARC_CHALLENGE
{
    public class Pilot
    {
        public string pilot;
        public LockoutCounter counter;
        public Pilot(string pilot) //we can either give it a lockout counter or have it create a lockout counter
        {
            this.pilot = pilot; //set pilot

            LoadCounter();   //initialize pilot counter from transient file. 

        }

        public void LoadCounter()
        {
            string jsonholder;
            //read transient file, set value. if value not found then add pilot to list and add increment. 
            jsonholder = FileProcessor.ProcessFile("C:/Users/micha/Documents/SPARC/TransientPilotInformation.csv"); //this filepath needs to be initialized somehow.

            JArray jsonArray = JArray.Parse(jsonholder);

            // Iterate over each object in the JSON array
            foreach (JObject obj in jsonArray)
            {
                // Get the values of the aircraft, name, and mission properties
                string objLockoutCount = obj.Value<string>("LockoutCount");
                string objPilot = obj.Value<string>("Pilot");

                // Check if there is a match
                if (objPilot == this.pilot)
                {
                    this.counter = new LockoutCounter(Convert.ToInt32(objLockoutCount));
                    return;
                }
            }

            this.counter = new LockoutCounter(0);
            return;

        }

        public void Increment()
        {
            //update increment
            this.counter.Increment();


            //update json and csv file

        }

        public void Reset()
        {
            //update increment
            this.counter.Reset();

            //update json and csv file
        }
    }
}