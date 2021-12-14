namespace VSoccerDemo.Models.Dto
{
    /// <summary>
    /// Model koji dolazi sa frontenda , sadrzi broj matcha i uplate na match
    /// </summary>
    public class TicketDto
        {
            /// <summary>
            /// Identifikacioni broj matcha
            /// </summary>
            public int MatchId { get; set; }
            /// <summary>
            /// Odigrane opklade na match, sadrzi listu id-eva koji se provjeravaju iz cahce-a
            /// </summary>
            public List<int> BetIds { get; set; }
            /// <summary>
            /// Ukupna uplata na tiketu
            /// </summary>
            public decimal PayIn { get; set; }
            /// <summary>
            /// Korisnik koji vrsi uplatu, jedinstveni string broj
            /// </summary>
            public string PayInOperator { get; set; }
        }
}
