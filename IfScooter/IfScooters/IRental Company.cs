namespace IfScooters
{
    public interface IRentalCompany
    {

        string Name { get; } //Name of the company

        void StartRent(string id); // start the rent of the scooter

        decimal EndRent(string id); // <returns>The total price of rental. It has to calculated taking into account for how long time scooter was rented. If total amount per day reaches 20 EUR than timer must be stopped and restarted at beginning of next day at 0:00 am.</returns>


        decimal CalculateIncome(int? year, bool includeNotCompletedRentals); // <param name="year">Year of the report. Sum all years if value is not set.</param> "includeNotCompletedRentals">Include income from the scooters that are rented out (rental has not ended yet) and calculate rental / price as if the rental would end at the time when this report was requested.</param> / <returns>The total price of all rentals filtered by year if given.</returns>
    }
}
