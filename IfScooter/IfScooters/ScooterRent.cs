using System;

namespace IfScooters
{
    public class ScooterRent : IRentalCompany
    {
        public static void Main() { } //Bez šī man neļāva palaist neko

        //Parameters
        public string Name { get; set; }
        private readonly ScooterService _scooty;

        //Constructor
        public ScooterRent(ScooterService scooty)
        {
            _scooty = scooty;
        }

        //Methods
        public void StartRent(string id)
        {
            _scooty.GetScooterById(id).IsRented = true;
            _scooty.RideHistory.Insert(0, new RideHistory(id));
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooty.GetScooterById(id);
            scooter.IsRented = false;
            double price = 0;
            foreach (var ride in _scooty.RideHistory)
            {
                if (ride.Id == id)
                {
                    ride.EndDateTime = DateTime.Now;
                }

                ride.CalculateScooterRideTime();
                price = ride.CalculateScooterTurnover(scooter);

                break;
            }

            decimal result = Convert.ToDecimal(price);
            return result;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal income = 0;
            if (includeNotCompletedRentals)
            {
                foreach (var scooter in _scooty.GetScooters())
                {
                    if (scooter.IsRented)
                    {
                        foreach (var ride in _scooty.RideHistory)
                        {
                            if (ride.Id == scooter.Id && ride.Turnover == 0)
                            {
                                ride.EndDateTime = DateTime.Now;
                                ride.CalculateScooterRideTime();
                                ride.CalculateScooterTurnover(scooter);
                            }
                        }
                    }
                }
            }

            foreach (var scooter in _scooty.RideHistory)
            {
                income += (decimal)scooter.Turnover;
            }
            return income;
        }
    }
}
