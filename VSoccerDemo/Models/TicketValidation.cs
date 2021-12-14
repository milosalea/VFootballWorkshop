namespace VSoccerDemo.Models
{
    public class TicketValidation
    {
        /// <summary>
        /// Minimalna Uplata na tiketu
        /// </summary>
        public decimal MinPayIn { get; set; } = 1;
        /// <summary>
        /// Maximalna uplata na tiketu
        /// </summary>
        public decimal MaxPayIn { get; set; } =  60;
        /// <summary>
        /// Minimalni broj opklada na tiketu
        /// </summary>
        public int MinNumberOfBetsOnTicket { get; set; } = 1;
        /// <summary>
        /// Maximalan broj opklada na tiketu
        /// </summary>
        public int MaxNumberOfBetsOnTickets { get; set; } = 6;
        /// <summary>
        /// Minimalna uplata po opkladi
        /// </summary>
        public decimal MinPayInPerItem { get; set; } = 1;
        /// <summary>
        /// Maximalna uplata po opkladi
        /// </summary>
        public decimal MaxPayInPerItem { get; set; } = 10;
    }
}
