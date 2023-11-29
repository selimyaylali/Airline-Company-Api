using Microsoft.EntityFrameworkCore;
using midterm_SE4458.Model;

namespace Repository
{
    public class FlightsRepository
    {
        public List<Flight> GetFlights(TicketQuery modal)
        {
            using var context = new SelimContext();
            var filter = new PaginationManager(modal.PageNumber, modal.PageSize);

            var pagedData = context.Flights
                .Where(flight => flight.FlightDate == modal.Date && flight.Departure == modal.Departure && flight.Destination == modal.Destination && flight.AvailableSeat >= modal.SoldTicket)
                .Skip((filter.minNumber - 1) * filter.maxNumber)
                .Take(filter.maxNumber)
                .ToList();

            return pagedData;
        }

        public Flight? UpdateFlightAttendants(int id)
        {
            using var context = new SelimContext();
            try
            {
                var flight = context.Flights.Find(id);
                if (flight != null)
                {
                    if (flight.AvailableSeat >= 1)
                    {
                        flight.AvailableSeat--;
                        context.Flights.Update(flight);
                        context.SaveChanges();
                    }
                }
                return flight;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Flight CreateFlight(Flight f)
        {
            var flight = new Flight { FlightDate = f.FlightDate, FlightNumber = f.FlightNumber, Departure = f.Departure, Destination = f.Destination, AvailableSeat = f.AvailableSeat, TotalSeat = f.TotalSeat, Price = f.Price };
            using var context = new SelimContext();
            context.Flights.Add(flight);
            context.SaveChanges();
            return f;
        }

        public string BuyTicket(BuyTicket ticket, Attendant attendant)
        {
            var status = "";
            using var context = new SelimContext();

            // Use FirstOrDefault instead of First
            var flight = context.Flights.FirstOrDefault(x => x.FlightNumber == ticket.FlightNo);
            if (flight != null)
            {
                // Use FirstOrDefault instead of First
                var flightClient = context.AttendantFlights.FirstOrDefault(x => x.FlightId == flight.FlightId && x.AttendantId == attendant.AttendantId);

                if (flightClient != null)
                {
                    status = "Client has already on the flight";
                }
                else
                {
                    var entity = context.AttendantFlights.Add(new AttendantFlight { AttendantId = attendant.AttendantId, FlightId = flight.FlightId });
                    context.SaveChanges();

                    if (entity != null)
                    {
                        var updatedFlight = UpdateFlightAttendants(flight.FlightId);
                        if (updatedFlight != null && updatedFlight.AvailableSeat < flight.AvailableSeat)
                        {
                            status = "Attendant is assigned to a flight.";
                        }
                        else
                        {
                            status = "There are no available seats left in the flight!";
                        }
                    }
                    else
                    {
                        status = "Attendant can not be assigned to the flight!";
                    }
                }
            }
            else
            {
                status = "Flight can not be found!";
            }
            return status;
        }
    }
}