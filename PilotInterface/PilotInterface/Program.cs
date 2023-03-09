using PilotInterface;

class Program
{
    public static void Main()
    {
        SmsMessageReceiver smsReceiver = new SmsMessageReceiver();

        // Run through as fast as possible, checking for new messages indefinitely.
        while (true)
        {
            
            smsReceiver.ReceiveMessage();
        }
    }
}