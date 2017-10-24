using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AlaskaAirlines.Models
{
    public class Csv
    {
        public static List<Flight> SearchFlights(string fromAirport, string toAirport)
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
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Error grabbing Flights CSV file:", ex);
            }
        }

        public static List<Airport> GetAllAirports()
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
            catch (FileNotFoundException ex)
            {
                throw new ApplicationException("Error grabbing Airports CSV file:", ex);
            }
        }
    }
}