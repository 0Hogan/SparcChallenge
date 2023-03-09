using OfficeOpenXml;
using System;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using PilotVerification;

namespace PilotVerification
{
    class PilotAuthorizer
    {
        /// <summary>
        /// Constructor. Doesn't do anything at this point in time.
        /// </summary>
        public PilotAuthorizer() {}

        /// <summary>
        /// Checks the authorization of the pilot to fly the given aircraft and mission (as specified in the message body).
        /// If authorized, loads the profile of the pilot onto the given aircraft. If unauthorized, notifies the pilot and a security officer
        /// of the unauthorized attempted access.
        /// </summary>
        /// <param name="pilotPhoneNumber">The pilot's phone number. (e.g. "+13168675309")</param>
        /// <param name="systemPhoneNumber">The system's phone number.</param>
        /// <param name="messageBody">The body of the message (i.e. the actual message; e.g. "X-Wing;DeathStarBombing")</param>
        public void CheckAuthorization (string pilotPhoneNumber, string systemPhoneNumber, string messageBody)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //do initialization
            Aircraft AC = new Aircraft();

            //initialize & create pilot array. Create a dictionary of pilots for easier lookup

            //create a dictionary of pilots
            Dictionary<string, Pilot> pilots = new Dictionary<string, Pilot>();

            //recieve message 

            //if not in dictionary then 
            //pilots.add(number, new pilot(number));

            //Varpilot = pilots(number);
            //Pilot varpilot = new Pilot("+13167229472");
            Pilot varpilot = new Pilot(pilotPhoneNumber);

            ProfileLoader loader = new ProfileLoader(varpilot, /*mission*/"ISR", AC.ID);
            loader.Load(AC);

        }
    }
}