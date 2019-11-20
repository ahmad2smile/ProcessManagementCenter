using System;
using System.Collections.Generic;

namespace ProcessManagementCenter.Domain
{
    public class Shift
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsProductionShift { get; set; }
        public DateTime TimeToFirstOperate { get; set; }
        public DateTime TimeToLastOperate { get; set; }

        public List<Miner> Miners { get; set; }
    }
}