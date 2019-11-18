using DreamLeague.Controllers;
using DreamLeague.Inputs;
using DreamLeague.Services;
using DreamLeague.Tests.DAL.Mock;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLeague.Tests.Controllers
{
    [TestFixture]
    public class TeamSheetControllerTests
    {
        MockDreamLeagueContext context;
        Mock<ITeamSheetService> teamSheetService;
        Mock<ITeamSheetUpdater> teamSheetUpdater;
        Mock<IPDFService> pdfService;
        Mock<IMeetingService> meetingService;
        TeamSheetController controller;

        [SetUp]
        public void SetUp()
        {
            context = new MockDreamLeagueContext();
            meetingService = new Mock<IMeetingService>();
            teamSheetService = new Mock<ITeamSheetService>();
            teamSheetUpdater = new Mock<ITeamSheetUpdater>();
            pdfService = new Mock<IPDFService>();
            controller = new TeamSheetController(context.MockContext.Object, teamSheetService.Object, teamSheetUpdater.Object, meetingService.Object, pdfService.Object);
        }

    }
}
