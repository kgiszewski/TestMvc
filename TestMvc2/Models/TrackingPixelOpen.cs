using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace TestMvc2.Models
{
    [PrimaryKey("id")]
    [TableName("TrackingPixelOpens")]
    public class TrackingPixelOpen
    {
        public long Id { get; set; }
        public string Campaign { get; set; }
        public string Source { get; set; }
        public string IpAddress { get; set; }
        public DateTime OpenedOn { get; set; }
    }
}