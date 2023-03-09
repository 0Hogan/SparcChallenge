using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SparcChallenge
{
    class SmsMessageTransmitter : MessageTransmitter
    {
        // Twilio Account Details (Saved in the environment):
        private string accountSid = "";
        private string authToken = "";
        private string sourceNumber = "";

        private string securityOfficerNumber = "+13166403501";

        /// <summary>
        /// Constructor for the SmsMessageTransmitter class. 
        /// </summary>
        public SmsMessageTransmitter() 
        {
            // Get the Twilio API details from the environment.
            accountSid = "AC93aa7249071b09e2c0bd29fd40649166";
            authToken = "b902534faef8d10361c344f45969b974";
            sourceNumber = "+13164459368";
            TwilioClient.Init(accountSid, authToken);
        }

        private void SendMessage(string number, string messageContents)
        {
            // Return if the number is null, as something is wrong. This should probably raise an exception instead.
            if (number == null)
                return;

            // Send the given message to the given number.
            var message = MessageResource.Create(
                body: messageContents,
                from: new Twilio.Types.PhoneNumber(sourceNumber),
                to: new Twilio.Types.PhoneNumber(number)
            );

        }

        /// <summary>
        /// Sends a notification to the pilot that their profile has been successfully loaded and that they are authorized to proceed.
        /// </summary>
        /// <param name="pilotNumber">The number of the pilot to whom the confirmation message should be sent.</param>
        public void SendConfirmation(string pilotNumber)
        {
            string pilotConfirmationMessage = "";
            // Notify the pilot that their profile has been successfully loaded and that they are authorized to proceed.
            SendMessage(pilotNumber, pilotConfirmationMessage);
            
            return;
        }

        /// <summary>
        /// Sends a notification to the pilot that their attempt to access the aircraft was rejected and sends another notification to a security officer detailing the attempt to access the aircraft.
        /// </summary>
        /// <param name="pilotNumber">The number (as a string) of the pilot trying to access the aircraft.</param>
        /// <param name="securityOfficerNumber">The number (as a string) of the security officer who should be notified of the unauthorized attempt to access the aircraft.</param>
        public void SendRejection(string pilotNumber)
        {
            // Get the current on-duty security officer's phone number.
            securityOfficerNumber = Environment.GetEnvironmentVariable("ON_DUTY_SECURITY_OFFICER_NUMBER");

            string pilotRejectionMessage = "";
            string securityOfficerRejectionMessage = "";

            // Notify the pilot that their attempt to access the aircraft/mission was rejected and that they are unauthorized.
            SendMessage(pilotNumber, pilotRejectionMessage);

            // Report the unauthorized attempt to access the aircraft/mission to the security officer.
            SendMessage(securityOfficerNumber, securityOfficerRejectionMessage);

            return;
        }

        /// <summary>
        /// Sends a notification to the pilot that their attempt to load their profile failed for some reason. This is different from a rejection notification, and does not update the lockout counter.
        /// </summary>
        /// <param name="pilotNumber"></param>
        public void SendFailure(string pilotNumber)
        {
            string pilotFailureMessage = "";

            // Notify the pilot that their profile failed to load.
            SendMessage(pilotNumber, pilotFailureMessage);

            return;
        }

    }
}
