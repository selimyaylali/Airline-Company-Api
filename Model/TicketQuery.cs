public class TicketQuery
{
    public DateTime Date { get; set; }
    public string Destination { get; set; }
    public string Departure { get; set; }
    public int SoldTicket { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}