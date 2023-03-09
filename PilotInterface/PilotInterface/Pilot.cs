using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotVerification
{
    public class Pilot
    {
        public string pilot;
        public LockoutCounter counter;
        private string transientfile;
        public Pilot(string pilot, Aircraft AC)
        {
            this.pilot = pilot; //set pilot

            LoadCounter(AC);   //initialize pilot counter from transient file. 
            this.transientfile = AC.SystemPaths.TransientPilotInformation;
        }

        public void LoadCounter(Aircraft AC)
        {
            string jsonholder;
            //read transient file, set value. if value not found then add pilot to list and add increment. 
            jsonholder = FileProcessor.ProcessFile(AC.SystemPaths.TransientPilotInformation);

            JArray jsonArray = JArray.Parse(jsonholder);

            // Iterate over each object in the JSON array
            foreach (JObject obj in jsonArray)
            {
                // Get the values of the aircraft, name, and mission properties
                string objLockoutCount = obj.Value<string>("LockoutCounter");
                string objPilot = obj.Value<string>("Pilot");

                // Check if there is a match
                if (objPilot == this.pilot.Substring(1))    //ignores the "+"
                {
                    this.counter = new LockoutCounter(Convert.ToInt32(objLockoutCount));
                    return;
                }
            }

            this.counter = new LockoutCounter(0);

            File.AppendAllText(AC.SystemPaths.TransientPilotInformation, this.pilot + "," + "0");
            return;

        }

        public void Increment()
        {
            //update increment
            this.counter.Increment();


            //update json and csv file
            UpdateFile(counter.NumLock());
        }

        public void Reset()
        {
            //update increment
            this.counter.Reset();


            //update json and csv file
            UpdateFile(counter.NumLock());
        }

        private void UpdateFile(int updatevalue)
        {
            var lines = File.ReadAllLines(this.transientfile);

            //find row we are looking for
            var row = lines
                    .Skip(1)
                    .Where(line => line.Split(',')[0].Trim() == this.pilot.Substring(1)) //find pilot row and ignore + sign
                    .FirstOrDefault(); //return first match or give null

            if (row != null)
            {

                var newRow = this.pilot.Substring(1) + "," + updatevalue.ToString();


                var rowIndex = Array.IndexOf(lines, row);

                lines[rowIndex] = newRow;

                //write updated lines back
                File.WriteAllLines(this.transientfile, lines);

            }
        }
    }
}