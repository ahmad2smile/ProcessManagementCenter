using System;
using System.Collections.Generic;

namespace ProcessManagementCenter.Domain
{
    public class MinerState
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int CurrentWork { get; set; }
        public int TargetWork { get; set; }
        public float OperatingRate { get; set; }
        public float CutDepth { get; set; }
        public float EosProjection { get; set; }

        public DateTime Timestamp { get; set; }
        public MineArea Area { get; set; }
        public MinerOperation Operation { get; set; }
        public MinerMode Mode { get; set; }
        public MinerStatus Status { get; set; }
        public List<MethaneState> MethaneStates { get; set; }

        public int MinerId { get; set; }
        public Miner Miner { get; set; }
    }
}
