using System;

namespace CT.Biuromat.Web.Areas.Administration.Models
{
    public class RoomEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string AccessCode { get; set; }

        public BuildingEntity Building { get; set; }
        public int BuildingId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}