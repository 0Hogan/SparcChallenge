using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PilotVerification
{
    class SmsMessageTransmitter : MessageTransmitter
    {
        // Twilio Account Details (Saved in the environment):
        private string accountSid = "";
        private string authToken = "";
        private string sourceNumber = "";

        private string securityOfficerNumber = "+13167274699";//"+13166403501";

        /// <summary>
        /// Constructor for the SmsMessageTransmitter class. 
        /// </summary>
        public SmsMessageTransmitter() 
        {
            // Get the Twilio API details from the environment.
            accountSid = "AC1f51f139fc6952500072981e333660d2";
            authToken = "1373310943acec5209b53ea53d65210e";
            sourceNumber = "+15675571239";
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
            string pilotConfirmationMessage = "Authorized to Fly";
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
            //securityOfficerNumber = Environment.GetEnvironmentVariable("ON_DUTY_SECURITY_OFFICER_NUMBER");

            string pilotRejectionMessage = "Not Authorized to Fly";
            string securityOfficerRejectionMessage = "Unauthorized person tried to access system";

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
            string pilotFailureMessage = "Failed to Load";

            // Notify the pilot that their profile failed to load.
            SendMessage(pilotNumber, pilotFailureMessage);

            return;
        }

    }
}
