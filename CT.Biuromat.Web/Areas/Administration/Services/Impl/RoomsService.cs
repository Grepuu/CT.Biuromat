using System;
using System.Collections.Generic;
using System.Linq;
using CT.Biuromat.Web.Areas.Administration.Models;
using CT.Biuromat.Web.Areas.Administration.ViewModels;
using CT.Biuromat.Web.Data;

namespace CT.Biuromat.Web.Areas.Administration.Services.Impl
{
    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext _dbContext;
        
        public RoomsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public RoomsIndexVm PrepareVmForIndex()
        {
            var list = GetAll();
            var model = new RoomsIndexVm()
            {
                Rooms = list
            };
            
            return model;
        }

        public RoomsDetailsVm PrepareVmForDetails(int id)
        {
            var room = GetOne(id);
            var model = new RoomsDetailsVm()
            {
                Room = room
            };
            
            return model;
        }

        public RoomsEditVm PrepareVmForEdit(int id)
        {
            var room = GetOne(id);
            var buildings = GetBuildings();
            var model = new RoomsEditVm()
            {
                Name = room.Name,
                AccessCode = room.AccessCode,
                BuildingId = room.BuildingId,
                Buildings = buildings
            };

            return model;
        }

        public RoomsCreateVm PrepareVmForCreate()
        {
            var buildings = GetBuildings();
            var model = new RoomsCreateVm()
            {
                Buildings = buildings
            };

            return model;
        }

        public ICollection<RoomEntity> GetAll()
        {
            var list = _dbContext.Rooms.Where(n => n.IsActive).ToList();
            return list;
        }

        public ICollection<RoomEntity> GetAllByBuildingId(int buildingId)
        {
            var list = _dbContext.Rooms.Where(n => n.IsActive)
                .Where(n => n.BuildingId == buildingId)
                .ToList();
            return list;
        }

        public RoomEntity GetOne(int id)
        {
            var entity = _dbContext.Rooms.FirstOrDefault(n => n.Id == id);
            return entity;
        }

        public void AddToDatabase(RoomsCreateVm model)
        {
            var dbModel = new RoomEntity();
            
            dbModel.CreatedAt = DateTime.UtcNow;
            dbModel.UpdatedAt = DateTime.UtcNow;
            dbModel.IsActive = true;
            dbModel.Name = model.Name;
            dbModel.AccessCode = model.AccessCode;
            dbModel.BuildingId = model.BuildingId;

            _dbContext.Rooms.Add(dbModel);
            _dbContext.SaveChanges();
        }

        public void UpdateRoom(RoomsEditVm model)
        {
            var dbModel = GetOne(model.Id);
            
            dbModel.UpdatedAt = DateTime.UtcNow;
            dbModel.Name = model.Name;
            dbModel.AccessCode = model.AccessCode;
            dbModel.BuildingId = model.BuildingId;

            _dbContext.Rooms.Update(dbModel);
            _dbContext.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            var dbModel = GetOne(id);
            
            dbModel.IsActive = false;
            dbModel.UpdatedAt = DateTime.UtcNow;

            _dbContext.Rooms.Update(dbModel);
            _dbContext.SaveChanges();
        }

        public void HardDeleteRoom(int id)
        {
            var dbModel = GetOne(id);

            _dbContext.Rooms.Remove(dbModel);
            _dbContext.SaveChanges();
        }

        private ICollection<BuildingEntity> GetBuildings()
        {
            return _dbContext.Buildings.Where(n => n.IsActive).ToList();
        }
    }
}