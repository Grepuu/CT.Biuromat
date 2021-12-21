using System.Collections.Generic;
using CT.Biuromat.Web.Areas.Administration.Models;
using CT.Biuromat.Web.Areas.Administration.ViewModels;

namespace CT.Biuromat.Web.Areas.Administration.Services
{
    public interface IRoomsService
    {
        // vm
        RoomsIndexVm PrepareVmForIndex();
        RoomsDetailsVm PrepareVmForDetails(int id);
        RoomsEditVm PrepareVmForEdit(int id);
        RoomsCreateVm PrepareVmForCreate();
        
        // baza danych
        ICollection<RoomEntity> GetAll();
        ICollection<RoomEntity> GetAllByBuildingId(int buildingId);
        RoomEntity GetOne(int id);
        
        // co zwraca? nazwa? (parametry?)
        void AddToDatabase(RoomsCreateVm model);
        void UpdateRoom(RoomsEditVm model);
        void DeleteRoom(int id);
        void HardDeleteRoom(int id);
    }
}