﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipTypes
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        //Reprezentacja pierwszych dwoch pol tabeli, 
        public static readonly byte Unknow = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}