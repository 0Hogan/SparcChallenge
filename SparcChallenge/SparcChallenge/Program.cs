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

        //do initialization
        Aircraft AC = new Aircraft();

        //initialize & create pilot array. Create a dictionary of pilots for easier lookup

        //create a dictionary of pilots
        Dictionary<string, Pilot> pilots = new Dictionary<string, Pilot>();

        //recieve message 

        //if not in dictionary then 
        //pilots.add(number, new pilot(number));

        //Varpilot = pilots(number);
        Pilot varpilot = new Pilot("+13167229472");

        ProfileLoader loader = new ProfileLoader(varpilot, /*mission*/"ISR", AC.ID);
        loader.Load(AC);

    }
}