﻿<using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationMessage:BaseEntity<long>
    {
        public string Message { get; set; }
        public DateTime PostDate { get; set; }

    }
}