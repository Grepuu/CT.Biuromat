using System.Collections.Generic;
using CT.Biuromat.Web.Areas.Administration.Models;

namespace CT.Biuromat.Web.Areas.Administration.ViewModels
{
    public class BuildingsDetailsVm
    {
        public BuildingEntity Building { get; set; }
        public ICollection<RoomEntity> Rooms { get; set; }
    }
}