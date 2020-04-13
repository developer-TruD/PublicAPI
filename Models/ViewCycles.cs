using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicAPI.Models
{
    public class ViewCycles
    {
        public int Id { get; set; }
        public string SessionNumber { get; set; }
        public int RoomID { get; set; }
        public string Room { get; set; }
        public string Technician { get; set; }
        public string Device { get; set; }
        public int ClientId { get; set; }
        public string CycleType { get; set; }
        public string ProtocolName { get; set; }
        public string PathogenShortName { get; set; }
        public string Name { get; set; }
        public decimal duration { get; set; }
        public DateTime CycleDate { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public DateTime MaxUpload { get; set; }
        public string EntityName { get; set; }
        public string Area { get; set; }
        public DateTime StartTime { get; set; }
    }
}
