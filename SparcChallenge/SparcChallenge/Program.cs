using OfficeOpenXml;
using System;
using System.IO;
using SPARC_CHALLENGE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //decode message body



        //do initialization
        Aircraft AC = new Aircraft();

        //initialize pilot
        //Varpilot = pilots(number);
        Pilot varpilot = new Pilot("+13167229472");
        string tempACreq = "F-35 F33";


        //if target ac is unavailable
        if (AC.Leader && !AC.inFleet(tempACreq))    //ac is not in the fleet and therefore unavailable
        {
            SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
            smsMessageTransmitter.SendFailure(varpilot.pilot);
            Console.WriteLine("unavailable");
            //will still need to update the lockout procedure even if the ac is unavailable.
        }else if(tempACreq==AC.ID && !AC.Available)
        {
            SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
            smsMessageTransmitter.SendFailure(varpilot.pilot);
            Console.WriteLine("unavailable");
            //will still need to update the lockout procedure even if the ac is unavailable.
        }



        ProfileLoader loader = new ProfileLoader(varpilot, /*mission*/"ISR", AC.ID);
        loader.Load(AC);

    }
}