using System.Collections.Generic;
using CT.Biuromat.Web.Areas.Administration.Models;

namespace CT.Biuromat.Web.Areas.Administration.ViewModels
{
    public class RoomsEditVm
    {
        // formularz
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public int BuildingId { get; set; }
        
        // dodatkowo wyświetlane dane

        public ICollection<BuildingEntity> Buildings { get; set; }
    }
}