using System.Collections.Generic;

namespace IfScooters
{

    public interface IScooterService
    {
        string Id { get; set; }
        decimal PricePerMinute { get; set; }

        void AddScooter(string id, decimal pricePerMinute);

        void RemoveScooter(string id);

        IList<Scooter> GetScooters();

        Scooter GetScooterById(string scooterId);

    }
}
