using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparcChallenge
{
    class SmsMessageTransmitter : MessageTransmitter
    {
        private string pilotConfirmationMessage = "";
        private string pilotRejectionMessage = "";
        private string securityOfficerRejectionMessage = "";
        private string pilotFailureMessage = "";

        /// <summary>
        /// Constructor for the SmsMessageTransmitter class. Does nothing by default.
        /// </summary>
        public SmsMessageTransmitter() { }

        private void SendMessage(string number, string message)
        {
            // @TODO: Clean up the number and format it for Twilio.


            // @TODO: Send the given message to the given number.

        }

        public void SendConfirmation(string pilotNumber)
        {
            // Notify the pilot that their profile has been successfully loaded and that they are authorized to proceed.
            SendMessage(pilotNumber, pilotConfirmationMessage);
            
            return;
        }

        /// <summary>
        /// Sends a notification to the pilot that their attempt to access the aircraft was rejected and sends another notification to a security officer detailing the attempt to access the aircraft.
        /// </summary>
        /// <param name="pilotNumber">The number (as a string) of the pilot trying to access the aircraft.</param>
        /// <param name="securityOfficerNumber">The number (as a string) of the security officer who should be notified of the unauthorized attempt to access the aircraft.</param>
        public void SendRejection(string pilotNumber, string securityOfficerNumber)
        {
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
            // Notify the pilot that their profile failed to load.
            SendMessage(pilotNumber, pilotFailureMessage);

            return;
        }

    }
}
