using PilotInterface;

class Program
{
    public static void Main()
    {
        SmsMessageReceiver smsReceiver = new SmsMessageReceiver();

        while (true)
        {
            smsReceiver.ReceiveMessage();
        }
    }
}