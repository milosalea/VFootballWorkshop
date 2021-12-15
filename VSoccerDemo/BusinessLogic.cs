using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using VSoccerDemo.Enums;
using VSoccerDemo.Models;
using VSoccerDemo.Models.Dto;

namespace VSoccerDemo
{
    /// <summary>
    /// Sadrzi biznis logiku aplikacije
    /// </summary>
    public class BusinessLogic
    {
        /// <summary>
        /// Generator slucajnih brojeva
        /// </summary>
        Random random { get; }
        /// <summary>
        /// Cache memorija, koristi se kao zamijena za bazu podataka
        /// </summary>
        private readonly IMemoryCache _cache;

        public BusinessLogic(IMemoryCache memoryCache)
        {
            random = new Random();
            _cache = memoryCache;
        }

        /// <summary>
        /// Izaberi match za trenutnu rundu
        /// </summary>
        /// <returns>Match model</returns>
        public Match GetCurrentMatch()
        {
            string user = Guid.NewGuid().ToString();
            //Uzmi sve meceve iz cache-a
            string json = File.ReadAllText("DemoData//CurrentMatches.json");
            //Deserijalizuj meceve iz json fajla u listu
            List<Match>? matchesList = JsonSerializer.Deserialize<List<Match>>(json);

            Match currentMatch = new Match();
            //Zadnji izabrani match
            var lastMatch = _cache.Get<Match>("CurrentMatch" + user);
            //Ako zadnji mec nije unesen , izaberi novi match
            if (lastMatch == null)
            {
                //Izaberi random match za trenutnu rundu
                currentMatch = matchesList[random.Next(matchesList.Count)];
                _cache.Set("CurrentMatch" + user, currentMatch);
            }
            else
            {
                //Izaberi random match za trenutnu rundu
                currentMatch = matchesList[random.Next(matchesList.Count)];
                _cache.Set("CurrentMatch" + user, currentMatch);
            }
            currentMatch.payInOperator = user;
            return currentMatch;
        }

        /// <summary>
        /// Uplata tiketa sa opkladama
        /// </summary>
        /// <param name="payInTicket"></param>
        /// <returns>True ako je tiket uspjesno uplacen , False ako nije</returns>
        public bool InsertTicket(TicketDto payInTicket)
        {
            string user = payInTicket.PayInOperator;
            //Validacijski model za provjeru da li je tiket ispravan
            var validationModel = new TicketValidation();
            //TODO: 1. Napraviti validaciju tiketa po vise parametara
            //TODO: 1.1 Provjera na tiketu za minimalnu i maximalnu uplatu
            //TODO: 1.2 Provjera za minimalan i maximalan broj tiketa
            //TODO: 1.3 Provjera po opkladi za minimalnu i maximalnu uplatu

            //if((payInTicket.MatchId / payInTicket.BetIds) ==)

            //1.1 task
            if ((payInTicket.PayIn >= validationModel.MinPayIn) && (payInTicket.PayIn <= validationModel.MaxPayIn)){
                return true;
            }
            else
            {
                return false;
            }

            //1.2 task
            if( (payInTicket.MatchId >= validationModel.MinNumberOfBetsOnTicket) && (payInTicket.MatchId <= validationModel.MaxNumberOfBetsOnTickets))
            {
                return true;
            }
            else
            {
                return false;
            }





            //Uzeti trenutan match iz cache-a
            Match? currentMatch = _cache.Get<Match>("CurrentMatch" + user);
            if(currentMatch != null)
            {
                var ticketCount = payInTicket.BetIds.Count();
                // Uplata na tiketu po opkladi
                decimal payInPerBet = payInTicket.PayIn / ticketCount;
                foreach (int betId in payInTicket.BetIds)
                {
                    //TODO: 2 Pronaci kvotu iz match modela po Id-u
                    //TODO: 2.1 Napraviti novi Ticket Model i popuniti sa podacima 
                    //TODO: 2.2 Dodati novu opkladu za korisnika u _cache-u za trenutni match sa kljucem bets + user

                    Ticket newTicket=new Ticket();
                    Stake stake = currentMatch.Stakes[betId];
                    newTicket.Id=Guid.NewGuid();
                    newTicket.MatchID = betId;
                    newTicket.PayIn = payInPerBet;
                    newTicket.StakeId = stake.Id;
                    newTicket.BetValue = stake.Value;

                         _cache.Set("bets" + user, newTicket);



                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Kalkulacija rezultata i obrada dobitnih tiketa (opklada)
        /// </summary>
        /// <param name="payInOperator">Korisnik koji salje request</param>
        /// <returns>Model sa podacima o rezultatu i visini dobitka</returns>
        public ResultDto CalculateResult(string payInOperator)
        {
            string user = payInOperator;

            //TODO: 3. Generisati rezultate za Full Time i Part Time, koristiti postojecu instancu random 
            int homeTeamRandom = 0;
            int awayTeamRandom = 0;



            //Task 3
            //Generisanje golova za domaci tim
            var homeFullTimeGoals = random.Next(0,8);
            //Generisanje rezultata za gosta
            var awayFullTimeGoals = random.Next(0,8);

            var homeHalfTimeGoals = random.Next(0, 8);
            var awayHalfTimeGoals = random.Next(0, 8);
            
            while(homeHalfTimeGoals > homeFullTimeGoals)
            {
                homeHalfTimeGoals = random.Next(0, 8);
            }

            while(awayHalfTimeGoals > awayFullTimeGoals)
            {
                awayHalfTimeGoals = random.Next(0, 8);

            }




            //Pokupi sve odigrane tikete iz cache-a za trenutni match 
            List<Ticket> betsOnMatch = _cache.Get<List<Ticket>>("bets" + user) ?? new List<Ticket>();
            //Ukupan dobitak za igraca
            decimal win = 0;
            foreach (var bet in betsOnMatch)
            {
                bool isWin = false;
                //TODO: 4. U Switchu provjeriti da li je opklada dobitna koristeci BetTypeEnum i golove za domacina/gosta , ako je dobitan setovati is win na true.
                switch (bet.StakeId) 
                {
                    case  ((int)BetTypeEnum.FullTimeAwayWin):
                        if(homeFullTimeGoals<awayFullTimeGoals)
                            isWin = true;
                        break;
                    case ((int)BetTypeEnum.HalfTimeAwayWin):
                        if (homeHalfTimeGoals < awayHalfTimeGoals)
                            isWin = true;
                        break;
                    case ((int)BetTypeEnum.FullTimeHomeWin):
                        if (homeFullTimeGoals > awayFullTimeGoals)
                            isWin = true;
                        break;
                    case ((int)BetTypeEnum.HalfTimeHomeWin):
                        if (homeHalfTimeGoals > awayHalfTimeGoals)
                            isWin = true;
                        break;
                    case ((int)BetTypeEnum.FullTimeDraw):
                        if (homeFullTimeGoals== awayFullTimeGoals)
                            isWin = true;
                        break;
                    case ((int)BetTypeEnum.HalfTimeDraw):
                        if (homeHalfTimeGoals == awayHalfTimeGoals)
                            isWin = true;
                        break;
                    default:
                        isWin = false;
                        break;

                }

                if (isWin)
                {
                    win += bet.BetValue * bet.PayIn;
                }
            }
            var result = new ResultDto
            {
                WinAmount = win,
                HalfTimeHomeGoals = homeHalfTimeGoals,
                HalfTimeAwayGoals = awayHalfTimeGoals,
                FullTimeHomeGoals = homeFullTimeGoals,
                FullTimeAwayGoals = awayFullTimeGoals,
            };
            //Ocisti tikete iz cache-a , runda je zavrsena
            _cache.Remove("bets" + user);
            return result;
        }
    }
}
