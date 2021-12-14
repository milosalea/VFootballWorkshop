namespace VSoccerDemo.Models
{
    public class Match
    {
        /// <summary>
        /// ID broj matcha
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Naziv domaceg tima
        /// </summary>
        public string HomeTeamName { get; set; }
        /// <summary>
        /// Naziv tima koji gostuje
        /// </summary>
        public string AwayTeamName { get; set; } 
        /// <summary>
        /// Lista kvota
        /// </summary>
        public List<Stake> Stakes { get; set; }
        /// <summary>
        /// Korisnik koji vrsi uplatu tiketa i provjeru meca
        /// </summary>
        public string payInOperator { get; set; }
    }
}
