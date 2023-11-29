using midterm_SE4458.Model;

namespace Repository
{
    public class AttendantFlightRepository
    {
       public bool IsAvailableToFlight(int userId, int flightId)
        {
            using var context = new SelimContext();
            try
            {
                var userFlight = context.AttendantFlights.First(u => u.AttendantId == userId && u.FlightId == flightId);
                return userFlight != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}