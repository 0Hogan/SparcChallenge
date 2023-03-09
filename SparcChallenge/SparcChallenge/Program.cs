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

        //decode message body "Aircraft; mission"

        string mission = "ISR";
        string ACReq = "F-16 F33";


        //do initialization
        Aircraft AC = new Aircraft();

        //initialize pilot
        //Varpilot = pilots(number);
        Pilot varpilot = new Pilot("+13167274699", AC);
        


        //if target ac is unavailable
        if (AC.Leader && !AC.inFleet(ACReq))    //ac is not in the fleet and therefore unavailable
        {
            SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
            smsMessageTransmitter.SendFailure(varpilot.pilot);
            //Console.WriteLine("unavailable");
            //will still need to update the lockout procedure even if the ac is unavailable.
        }else if(ACReq==AC.ID && !AC.Available)
        {
            SmsMessageTransmitter smsMessageTransmitter = new SmsMessageTransmitter();
            smsMessageTransmitter.SendFailure(varpilot.pilot);
            //Console.WriteLine("unavailable");
            //will still need to update the lockout procedure even if the ac is unavailable.
        }



        ProfileLoader loader = new ProfileLoader(varpilot, mission, ACReq);
        loader.Load(AC);

    }
}