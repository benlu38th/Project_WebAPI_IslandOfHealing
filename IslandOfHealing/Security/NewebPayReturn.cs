﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Security
{
    public class NewebPayReturn
    {
        public string Status { get; set; }
        public string MerchantID { get; set; }
        public string Version { get; set; }
        public string TradeInfo { get; set; }
        public string TradeSha { get; set; }
    }
}