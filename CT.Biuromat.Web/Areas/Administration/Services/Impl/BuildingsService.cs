using System;
using System.Collections.Generic;
using System.Linq;
using CT.Biuromat.Web.Areas.Administration.Models;
using CT.Biuromat.Web.Areas.Administration.ViewModels;
using CT.Biuromat.Web.Data;

namespace CT.Biuromat.Web.Areas.Administration.Services.Impl
{
    public class BuildingsService : IBuildingsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRoomsService _roomsService;
        
        public BuildingsService(ApplicationDbContext dbContext, IRoomsService roomsService)
        {
            _dbContext = dbContext;
            _roomsService = roomsService;
        }

        public BuildingsIndexVm PrepareVmForIndex()
        {
            var list = GetAll();
            var model = new BuildingsIndexVm()
            {
                Buildings = list
            };
            
            return model;
        }

        public BuildingsDetailsVm PrepareVmForDetails(int id)
        {
            var building = GetOne(id);
            var rooms = _roomsService.GetAllByBuildingId(id);
            var model = new BuildingsDetailsVm()
            {
                Building = building,
                Rooms = rooms
            };
            
            return model;
        }

        public BuildingsEditVm PrepareVmForEdit(int id)
        {
            var building = GetOne(id);
            var model = new BuildingsEditVm()
            {
                Name = building.Name,
                Address = building.Address
            };

            return model;
        }

        public ICollection<BuildingEntity> GetAll()
        {
            var list = _dbContext.Buildings.Where(n => n.IsActive).ToList();
            return list;
        }

        public BuildingEntity GetOne(int id)
        {
            var entity = _dbContext.Buildings.FirstOrDefault(n => n.Id == id);
            return entity;
        }

        public void AddToDatabase(BuildingsCreateVm model)
        {
            // tworzenie modelu
            var dbModel = new BuildingEntity();
            
            // uzupełnianie automatyczne
            dbModel.CreatedAt = DateTime.UtcNow;
            dbModel.UpdatedAt = DateTime.UtcNow;
            dbModel.IsActive = true;
            
            // przypisanie rzeczy które wpisuje user
            dbModel.Name = model.Name;
            dbModel.Address = model.Address;

            _dbContext.Buildings.Add(dbModel);
            _dbContext.SaveChanges();
        }

        public void UpdateBuilding(BuildingsEditVm model)
        {
            // pobieranie aktualnego modelu z bazy
            var dbModel = GetOne(model.Id);
            
            // uzupełnianie automatyczne
            dbModel.UpdatedAt = DateTime.UtcNow;
            
            // przypisanie rzeczy które wpisuje user
            dbModel.Name = model.Name;
            dbModel.Address = model.Address;

            _dbContext.Buildings.Update(dbModel);
            _dbContext.SaveChanges();
        }

        public void DeleteBuilding(int id)
        {
            // pobieranie aktualnego modelu z bazy
            var dbModel = GetOne(id);
            
            // uzupełnianie automatyczne
            dbModel.IsActive = false;
            dbModel.UpdatedAt = DateTime.UtcNow;

            _dbContext.Buildings.Update(dbModel);
            _dbContext.SaveChanges();
        }

        public void HardDeleteBuilding(int id)
        {
            // pobieranie aktualnego modelu z bazy
            var dbModel = GetOne(id);
            
            // usuwanie podmodeli
            

            _dbContext.Buildings.Remove(dbModel);
            _dbContext.SaveChanges();
        }
    }
}