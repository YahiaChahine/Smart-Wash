namespace SmartWashProject.Entities
{
    public class Machine
    {
        public int ID { get; set; }
        public MachineType Type { get; set; }
        public bool IsAvailable { get; set; }
    }
    public enum MachineType
    {
        WashingMachine = 0,
        DryingMachine = 1
    }
}
