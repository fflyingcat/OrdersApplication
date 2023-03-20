namespace DataAccess.Entities
{
    public class Provider
    {
        public Provider()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}