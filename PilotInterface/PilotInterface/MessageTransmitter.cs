using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotVerification
{
    class MessageTransmitter
    {
        public MessageTransmitter() {}

        /// <summary>
        /// Print a specified message to the Console.
        /// </summary>
        /// <param name="message"></param>
        public virtual void SendMessage(string message)
        {
            Console.WriteLine(message);
            return;
        }

        /// <summary>
        /// Print a rejection message to the Console. Intended to communicate that the pilot is not authorized to fly the aircraft/mission.
        /// </summary>
        public virtual void SendRejection()
        {
            SendMessage("Access denied.");
            return;
        }

        /// <summary>
        /// Print a failure message to the Console. Intended to communicate a failure to retrieve the pilot's profile.
        /// </summary>
        public virtual void SendFailure()
        {
            SendMessage("Failed to retrieve pilot profile.");
        }

        /// <summary>
        /// Print a confirmation message to the Console. Intended to communicate a successful retrieval of the pilot's profile.
        /// </summary>
        public virtual void SendConfirmation()
        {
            SendMessage("Access granted.");
            return;
        }


    }
}
