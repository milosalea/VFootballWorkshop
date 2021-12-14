namespace VSoccerDemo.Models.Dto
{
    /// <summary>
    /// Model koji se vraca na frontend , sadrzi dobitak za igraca i rezultat matcha
    /// </summary>
    public class ResultDto
    {
        /// <summary>
        /// Dobitak igraca, ako je 0 onda je tiket ne dobitni
        /// </summary>
        public decimal WinAmount { get; set; }
        /// <summary>
        /// Broj golova na poluvremenu za domacina
        /// </summary>
        public int HalfTimeHomeGoals { get; set; }
        /// <summary>
        /// Broj golova na poluvremenu za gosta
        /// </summary>
        public int HalfTimeAwayGoals { get; set; }
        /// <summary>
        /// Broj golova na kraju utakmice za domacina
        /// </summary>
        public int FullTimeHomeGoals { get; set; }
        /// <summary>
        /// Broj golova na kraju utakmice za gosta
        /// </summary>
        public int FullTimeAwayGoals { get; set; }  
    }
}
