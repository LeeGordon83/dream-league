﻿using DreamLeague.DAL;
using DreamLeague.Models;
using DreamLeague.Models.Api;
using DreamLeague.Services;
using DreamLeague.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DreamLeague.Controllers
{
    public class DataController : ApiController
    {
        DreamLeagueContext db;
        IGameWeekSerializer<GameWeekSummary> gameWeekSerializer;
        IGameWeekService gameWeekService;

        public DataController()
        {
            this.db = new DreamLeagueContext();
            this.gameWeekSerializer = new XMLGameWeekSerializer<GameWeekSummary>();
            this.gameWeekService = new GameWeekService(db);
        }

        public DataController(DreamLeagueContext db, IGameWeekSerializer<GameWeekSummary> gameWeekSerializer, IGameWeekService gameWeekService)
        {
            this.db = db;
            this.gameWeekSerializer = gameWeekSerializer;
            this.gameWeekService = gameWeekService;
        }

        [HttpGet]
        public List<Winner> Winners()
        {
            List<Winner> winners = new List<Winner>();

            var gameWeeks = db.GameWeeks.AsNoTracking().Where(x => x.Complete);

            foreach (var gameWeek in gameWeeks)
            {
                var gameWeekSummary = gameWeekSerializer.DeSerialize(gameWeek.Number, "GameWeek");

                foreach (var winner in gameWeekSummary?.Winners)
                {
                    winners.Add(new Winner { GameWeek = gameWeek.Number, Name = winner });
                }
            }

            return winners;
        }

        [HttpGet]
        public List<TableRow> Table()
        {
            var gameWeek = gameWeekService.GetLatest();

            if (gameWeek != null)
            {
                var gameWeekSummary = gameWeekSerializer.DeSerialize(gameWeek.Number, "GameWeek");

                return gameWeekSummary?.Table?.TableRows;
            }

            return null;
        }

        [HttpGet]
        public List<Meeting> Meetings()
        {
            return db.Meetings.OrderBy(x => x.MeetingId).ToList();
        }
    }
}
