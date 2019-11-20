namespace ProcessManagementCenter.Domain
{
    public class MethaneState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Value { get; set; }
        public int SortOrder { get; set; }

        public int MinerStateId { get; set; }
        public MinerState MinerState { get; set; }
    }
}