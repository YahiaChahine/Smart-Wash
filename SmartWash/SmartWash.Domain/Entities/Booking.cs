using Microsoft.AspNetCore.Identity;

namespace SmartWash.Domain.Entities
{
    public class Booking
    {
        public int ID { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime ?StartTime { get; set; }

        public DateTime? EndTime => StartTime.HasValue ? 
            StartTime.Value.AddMinutes(Constants.CycleDurationMinutes * CycleNum) : null;

        public int MachineId { get; set; }
        public Machine Machine { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CycleNum { get; set; }
        public string ?AccessPassword { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCancelled { get; set; }

        public void Cancel()
        {
            if (StartTime.HasValue)
            {
                throw new InvalidOperationException("Booking has already started");
            }

            IsCancelled = true;
        }
    }
}
