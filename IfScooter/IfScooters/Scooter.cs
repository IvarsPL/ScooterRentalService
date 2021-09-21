namespace IfScooters
{
    public class Scooter
    {
        //Parameters
        public string Id { get; }
        public decimal PricePerMinute { get; set; }
        public bool IsRented { get; set; }
        //Constructor
        public Scooter(string id, decimal pricePerMinute)
        {
            Id = id;
            PricePerMinute = pricePerMinute;
            IsRented = false;
        }


    }
}
