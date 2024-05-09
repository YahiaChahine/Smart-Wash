namespace SmartWashProject.Entities
{
    public class Booking
    {
        public int ID { get; set; }
        public DateTime BookingDateTime { get; set; }
        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CycleNum { get; set; }
        public string ?AccessPassword { get; set; }
        public bool IsPaid { get; set; }

    }
}
