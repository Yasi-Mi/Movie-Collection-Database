using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MovieDatabase.Web.Code
{
    public static class EPPlusExtensions
    {
        /// <summary>
        /// Creates a worksheet with a Vladimir Arts header.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="sheetName">The name of the worksheet</param>
        /// <param name="title">The title that appears on the 2nd line of the header</param>
        /// <param name="subtitle">A subtitle that appears just below the title.</param>
        /// <returns></returns>
        public static ExcelWorksheet CreateSheet(this ExcelPackage p, string sheetName, string title, string subtitle)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[1];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
            ws.Cells[1, 1].Value = "Date: " + DateTime.Now.ToShortDateString();
            
            ws.Cells[2, 1].Value = "Time: " + DateTime.Now.ToShortTimeString();
            ws.Cells[1, 2].Value = "Vladimir Arts U.S.A, Inc.";
            ws.Cells[2, 2].Value = title;
            ws.Cells[3, 2].Value = subtitle;
            ws.Cells["B1:B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells["B1:L1"].Merge = true;
            ws.Cells["B2:L2"].Merge = true;
            ws.Cells["B3:L3"].Merge = true;

            ws.Cells["A1:L4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1:L4"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            return ws;
        }
    }
}