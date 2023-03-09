using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;

namespace PilotVerification
{
    public static class FileProcessor
    {
        public static string ProcessFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            switch (extension)
            {
                case ".xlsx":
                    return ProcessExcel(filePath);
                    break;
                case ".csv":
                    return ProcessCSV(filePath);
                //add support for csv
                //add support for other file types here
                default:
                    throw new ArgumentException($"Unsupported file type: {extension}");
            }
        }

        private static string ProcessCSV(string filePath)
        {
            string[] csvLines = File.ReadAllLines(filePath);
            //get header row 
            string[] headers = csvLines[0].Split(',');

            //create list for rows
            List<Dictionary<string,string>> list = new List<Dictionary<string,string>>();

            //loop through each row
            for (int i = 1; i < headers.Length; i++)
            {
                //split row into fields
                string[] fields = csvLines[i].Split(',');

                //create dictionary to hold field values
                Dictionary<string,string> row = new Dictionary<string,string>();

                //loop through each field in row
                for(int j = 0; j < headers.Length; j++)
                {
                    row.Add(headers[j], fields[j]);
                }

                //add the row to data list
                list.Add(row);
            }

            //convert to JSON
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            return json;
        }

            private static string ProcessExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //open excel file
            FileInfo fileinfo = new FileInfo(filePath);
            using ExcelPackage excelPackage = new ExcelPackage(fileinfo);

            //get first worksheet
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            //get num rows and cols in worksheet
            int rowCount = worksheet.Dimension.Rows;
            int columnCount = worksheet.Dimension.Columns;

            //create string array to hold cell values
            string[,] cellValues = new string[rowCount, columnCount];

            //loop through cells and populate cellValues array
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
                {
                    object cellValue = worksheet.Cells[i, j].Value;
                    cellValues[i - 1, j - 1] = cellValue?.ToString();
                }
            }

            //call the process function with the cellValues array
            return ProcessExcelToJson(cellValues);
            
        }

        static string ProcessExcelToJson(object[,] cellValues)
        {
            List<Dictionary<string, string>> rowData = new List<Dictionary<string, string>>();

            // get column names from first row of cellValues array
            string[] columnNames = new string[cellValues.GetLength(1)];
            for (int j = 0; j < cellValues.GetLength(1); j++)
            {
                columnNames[j] = cellValues[0, j].ToString();
            }

            // loop through rows of cellValues array and add to rowData list
            for (int i = 1; i < cellValues.GetLength(0); i++)
            {
                Dictionary<string, string> rowDict = new Dictionary<string, string>();
                for (int j = 0; j < cellValues.GetLength(1); j++)
                {
                    string columnName = columnNames[j];
                    string cellValue = cellValues[i, j]?.ToString();
                    rowDict.Add(columnName, cellValue);
                }
                rowData.Add(rowDict);
            }

            string json = JsonConvert.SerializeObject(rowData, Formatting.Indented);
            //Console.WriteLine(json);
            return json;
        }
    }
}
