using System.Collections.Generic;

namespace ProcessManagementCenter.Domain
{
    public class Miner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public MinerType Type { get; set; }
        public WorkUnit WorkUnit { get; set; }
        public List<MinerState> States { get; set; }
    }
}
