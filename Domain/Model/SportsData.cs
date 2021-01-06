using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public class SportsData
    {
        public int GameId { get; set; }
        public int Season { get; set; }
        public int SeasonType { get; set; }
        public DateTime Day { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamName { get; set; }
        public int GlobalGameId { get; set; }
        public int GlobalAwayTeamId { get; set; }
        public int GlobalHomeTeamId { get; set; }
        public object HomeTeamScore { get; set; }
        public object AwayTeamScore { get; set; }
        public object TotalScore { get; set; }
        public int HomeRotationNumber { get; set; }
        public int AwayRotationNumber { get; set; }
        public List<PregameOdd> PregameOdds { get; set; }
        public List<object> LiveOdds { get; set; }
        public List<object> AlternateMarketPregameOdds { get; set; }


        public class PregameOdd
        {
            public int GameOddId { get; set; }
            public string Sportsbook { get; set; }
            public int GameId { get; set; }
            public DateTime Created { get; set; }
            public DateTime Updated { get; set; }
            public int HomeMoneyLine { get; set; }
            public int AwayMoneyLine { get; set; }
            public double HomePointSpread { get; set; }
            public double AwayPointSpread { get; set; }
            public int HomePointSpreadPayout { get; set; }
            public int AwayPointSpreadPayout { get; set; }
            public double OverUnder { get; set; }
            public int OverPayout { get; set; }
            public int UnderPayout { get; set; }
            public int SportsbookId { get; set; }
            public string OddType { get; set; }
            public string SportsbookUrl { get; set; }
        }
    }
}
