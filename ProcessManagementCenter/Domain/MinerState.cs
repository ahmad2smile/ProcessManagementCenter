using System;
using System.Collections.Generic;

namespace ProcessManagementCenter.Domain // Model
{
    public class MinerState
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int CurrentWork { get; set; }
        public int TargetWork { get; set; }
        public float OperatingRate { get; set; } // Calculate on fly (WorkDone/Hour)
        public float CutDepth { get; set; }
        public float EosProjection { get; set; } // Calculate on fly (WorkDone right until now and how much it would be at ttfo)

        public DateTime Timestamp { get; set; }
        public MineArea Area { get; set; }
        public MinerOperation Operation { get; set; } // Can have multiple operations at a time
        public MinerMode Mode { get; set; }
        public MinerStatus Status { get; set; }
        public List<MethaneState> MethaneStates { get; set; }

        public int MinerId { get; set; }
        public Miner Miner { get; set; }
    }
}
