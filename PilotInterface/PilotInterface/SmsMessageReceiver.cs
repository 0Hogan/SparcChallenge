using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PilotInterface
{
    class SmsMessageReceiver
    {
        // Twilio Account Details (Saved in the environment):
        private string accountSid = "";
        private string authToken = "";
        private string systemNumber = "";
        private MessageResource? lastMessage = null;
        /// <summary>
        /// Constructor for the SmsMessageReceiver class.
        /// </summary>
        public SmsMessageReceiver()
        {
            // Get the Twilio API details from the environment.
            accountSid = "AC93aa7249071b09e2c0bd29fd40649166";
            authToken = "b902534faef8d10361c344f45969b974";
            systemNumber = "+13164459368";
            TwilioClient.Init(accountSid, authToken);
            lastMessage = MessageResource.Read(limit: 1).First();
        }

        public void ReceiveMessage()
        {
            var message = MessageResource.Read(limit:1).First();

            if (lastMessage == null || lastMessage.Sid != message.Sid)
            {

                var pilotNumber = message.From;
                var messageBody = message.Body;


                // Replace this with a call to start the Pilot Verification Process thread.
                Console.Write("Message sent from: ");
                Console.WriteLine(pilotNumber);
                Console.Write("Body: ");
                Console.WriteLine(messageBody);

                lastMessage = message;
            }
        }

    }
}
