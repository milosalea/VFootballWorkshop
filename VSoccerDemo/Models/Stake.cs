namespace VSoccerDemo.Models
{
    public class Stake
    {
        /// <summary>
        /// ID kvote
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Naziv kvote
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tip kvote
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Vrijednost kvote
        /// </summary>
        public decimal Value { get; set; }
    }
}
