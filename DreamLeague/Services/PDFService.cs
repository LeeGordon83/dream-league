using DreamLeague.DAL;
using DreamLeague.Inputs;
using DreamLeague.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DreamLeague.Services
{
    public class PDFService : IPDFService
    {
        readonly IDreamLeagueContext db;
        readonly IMeetingService meetingService;


        public PDFService(IDreamLeagueContext db, IMeetingService meetingService)
        {
            this.db = db;
            this.meetingService = meetingService;
        }

        public void SealedBidBuilder(TeamSheet teamSheet)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads", "Sealed Bid Form Html.htm");

     

            var renderer = new IronPdf.HtmlToPdf();
            renderer.PrintOptions.CreatePdfFormsFromHtml = true;
            var PDF = renderer.RenderHTMLFileAsPdf(filePath);

            // Set the value of the "Venue" field
            var venueField = PDF.Form.GetFieldByName("venue");
            venueField.Value = meetingService.Next().Location;
            venueField.ReadOnly = true;

            // Set the value of the "Date" field
            var dateField = PDF.Form.GetFieldByName("meetingDate");
            dateField.Value = meetingService.Next().Date.ToString();
            dateField.ReadOnly = true;

            // Set the value of the "name" field
            var nameField = PDF.Form.GetFieldByName("name");
            nameField.Value = "";

            // Set all other fields up
            var position1Field = PDF.Form.GetFieldByName("position1");
            var position2Field = PDF.Form.GetFieldByName("position2");
            var forename1Field = PDF.Form.GetFieldByName("forename1");
            var forename2Field = PDF.Form.GetFieldByName("forename2");
            var surname1Field = PDF.Form.GetFieldByName("surname1");
            var surname2Field = PDF.Form.GetFieldByName("surname2");
            var club1Field = PDF.Form.GetFieldByName("club1");
            var club2Field = PDF.Form.GetFieldByName("club2");

            //Set the manager name and balance

            for (int i = 0; i < teamSheet.Teams.Count(); i++)
            {
                int x = i + 1;

                var managerField = PDF.Form.GetFieldByName("manager" + x);
                managerField.Value = teamSheet.Teams[i].Manager;
                managerField.ReadOnly = true;

                var balanceField = PDF.Form.GetFieldByName("balance" + x);
                balanceField.Value = teamSheet.Teams[i].ManagerBalance;
                balanceField.ReadOnly = true;
            }

    

            for(int p = teamSheet.Teams.Count(); p < 14; p++)
            {
                int z = p + 1;

                var managerField = PDF.Form.GetFieldByName("manager" + z);
                managerField.Value = "";
                managerField.ReadOnly = true;

                var balanceField = PDF.Form.GetFieldByName("balance" + z);
                balanceField.Value = "";
                balanceField.ReadOnly = true;
            }

            string path = String.Format("{0}Downloads/Sealed Bid Form.pdf", AppDomain.CurrentDomain.BaseDirectory);

          
            PDF.SaveAs(path);
        }
    }
}