using DreamLeague.DAL;
using DreamLeague.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamLeague.Controllers
{
    public class SealedBidController : DownloadController
    {
        readonly IDreamLeagueContext db;
        readonly IPDFService sealedBidService;
        readonly IMeetingService meetingService;

        public SealedBidController()
        {
            this.db = new DreamLeagueContext();
            this.meetingService = new MeetingService(db);
            this.sealedBidService = new PDFService(db, meetingService);
        }

        public SealedBidController(IDreamLeagueContext db, IMeetingService meetingService)
        {
            this.db = db;
            this.meetingService = meetingService;
        }

        
    }
}