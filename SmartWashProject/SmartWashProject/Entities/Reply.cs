namespace SmartWashProject.Entities
{
    public class Reply
    {
        public int ID { get; set; }
        public int FeedbackId { get; set; }
        public Feedback Feedback { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public string ?Content { get; set; }
        public DateTime ReplyDateTime { get; set; }
    }
}
