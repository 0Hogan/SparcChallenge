using OfficeOpenXml;
using System;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using PilotVerification;
using Twilio;

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

            //decode message body "Aircraft; mission"

            string mission;
            string ACReq;

            //do initialization
            Aircraft AC = new Aircraft();

            //decipher message
            string[] partition = messageBody.Split(';');

            if (partition.Length > 0)
            {
                mission = partition[1];
                ACReq = partition[0];

            }
            else
            {
                if (AC.Leader)
                {
                    SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
                    smsMessageTransmitter.SendFailure(pilotPhoneNumber);
                }
                return;
            }

           

            //initialize pilot
            //Varpilot = pilots(number);
            Pilot varpilot = new Pilot(pilotPhoneNumber, AC);



            //if target ac is unavailable
            if (AC.Leader && !AC.inFleet(ACReq))    //ac is not in the fleet and therefore unavailable
            {
                SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
                smsMessageTransmitter.SendFailure(varpilot.pilot);
                
                //will still need to update the lockout procedure even if the ac is unavailable.
            }
            else if (ACReq == AC.ID && !AC.Available)
            {
                SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
                smsMessageTransmitter.SendFailure(varpilot.pilot);
                
                //will still need to update the lockout procedure even if the ac is unavailable.
            }



            ProfileLoader loader = new ProfileLoader(varpilot, mission, ACReq);
            loader.Load(AC);

            string accountSid = "AC93aa7249071b09e2c0bd29fd40649166";
            string authToken = "b902534faef8d10361c344f45969b974";
            string systemNumber = "+13164459368";
            TwilioClient.Init(accountSid, authToken);

            return;
        }
    }
}