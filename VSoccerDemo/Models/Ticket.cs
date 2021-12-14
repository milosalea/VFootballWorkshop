namespace VSoccerDemo.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ID trenutnog Matcha
        /// </summary>
        public int MatchID { get; set; }
        /// <summary>
        /// ID kvote u mecu
        /// </summary>
        public int StakeId { get; set; }
        /// <summary>
        /// Vrijednost kvote 
        /// </summary>
        public decimal BetValue { get; set; }
        /// <summary>
        /// Uplata na kvotu
        /// </summary>
        public decimal PayIn { get; set; }
    }
}
