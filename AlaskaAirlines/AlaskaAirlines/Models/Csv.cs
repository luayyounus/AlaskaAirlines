using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace AlaskaAirlines.Models
{
    public class Csv
    {
        public List<Flight> SearchFlights(string fromAirport, string toAirport)
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Models/Csv/flights.csv"))
                {
                    var csv = new CsvReader(reader);
                    IEnumerable<Flight> flights = csv.GetRecords<Flight>();

                    List<Flight> filteredFlights = new List<Flight>();

                    foreach (Flight flight in flights.Where(fl => fl.From == fromAirport && fl.To == toAirport))
                    {
                        filteredFlights.Add(flight);
                    }
                    return filteredFlights;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Error grabbing Flights CSV file:", ex);
            }
        }

        public List<Airport> GetAllAirports()
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Models/Csv/airports.csv"))
                {
                    var csv = new CsvReader(reader);
                    IEnumerable<Airport> airports = csv.GetRecords<Airport>();

                    List<Airport> allAirports = new List<Airport>();

                    foreach (Airport airport in airports)
                    {
                        allAirports.Add(airport);
                    }
                    return allAirports;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Error grabbing Airports CSV file:", ex);
            }
        }

        public Dictionary<string, string> GetAllAirportsDictionary()
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/Models/Csv/airports.csv"))
                {
                    var csv = new CsvReader(reader);
                    IEnumerable<Airport> airports = csv.GetRecords<Airport>();

                    Dictionary<string, string> allAirports = new Dictionary<string, string>();

                    foreach (Airport airport in airports)
                    {
                        allAirports.Add(airport.Name, airport.Code);
                    }
                    return allAirports;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Error grabbing Airports CSV file:", ex);
            }
        }

        public string GetCodeForName(string airportName)
        {
            Dictionary<string, string> allAirports = GetAllAirportsDictionary();

            if(!allAirports.ContainsKey(airportName))
            {
                return string.Empty;
            }

            return allAirports[airportName];
        }

        public List<Flight> SortFlights(List<Flight> flights,string sortType)
        {
            List<Flight> sortedFlights = new List<Flight>();

            switch (sortType)
            {
                case "Flight":
                    sortedFlights = flights.OrderBy(flight => flight.FlightNumber).ToList();
                    break;
                case "Departure":
                    sortedFlights = flights.OrderBy(flightTime => flightTime.Departs.TimeOfDay).ToList();
                    break;
                case "Price":
                    sortedFlights = flights.OrderBy(flightPrice => flightPrice.MainCabinPrice).ToList();
                    break;
            }
            return sortedFlights;
        }
    }
}