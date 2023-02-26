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


string filePath = "C:/Users/micha/Documents/SPARC/SPARCPilotLog.xlsx";
FileProcessor.ProcessFile(filePath);
