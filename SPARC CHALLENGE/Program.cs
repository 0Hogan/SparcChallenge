// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using OfficeOpenXml;
using System;
using System.IO;
using SPARC_CHALLENGE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


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



/*

string filePath = "C:/Users/micha/Documents/SPARC/SPARCPilotLog.xlsx";
FileProcessor.ProcessFile(filePath);



Console.WriteLine("I'm a program!");
Console.WriteLine("I am finished.");
LockoutCounter variable = new LockoutCounter();

Aircraft ac = new Aircraft("C:/Users/micha/Documents/SPARC/ACID.txt");

Console.WriteLine(ac.ID);
*/
/*
variable.Increment();
if (true == variable.IsLocked)
{
    Console.WriteLine("locked out.");
}

//variable.Increment();
variable.Increment();
if (true == variable.IsLocked)
{
    Console.WriteLine("locked out 2.");
}
ProfileLoader loader = new ProfileLoader("john", "SR", "F-16 F33", variable);

loader.Load(ac);

variable.Increment();
variable.Increment();
variable.Increment();
loader.Load(ac);
*/