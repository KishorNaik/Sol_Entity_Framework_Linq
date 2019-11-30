﻿using System;
using System.Collections.Generic;

namespace Sol_EF_Core.DbModels.DBEntities
{
    public partial class VProductModelInstructions
    {
        public int ProductModelId { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public int? LocationId { get; set; }
        public decimal? SetupHours { get; set; }
        public decimal? MachineHours { get; set; }
        public decimal? LaborHours { get; set; }
        public int? LotSize { get; set; }
        public string Step { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
