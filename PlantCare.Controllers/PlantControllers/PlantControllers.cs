﻿using PlantCare.DataAccess.models;
using PlantCare.DataAccess.services;
using PlantCare.DataAccess;

namespace PlantCare.Controllers
{
    public class PlantControllers : IPlantControllers
    {
        private IView _view;
        private IPlantServices _plantServices;

        public PlantControllers(IView view)
        {
            _view = view;
            _plantServices = DataAccessFactory.GetPlantServicesInstance();
        }

        public List<Plant> GetPlants() => _plantServices.GetPlants();
        public List<Plant> GetRoomPlants(Guid Id)
        {
            var plants = _plantServices.GetPlants();
            return plants.Where(plant => plant.RoomID == Id).ToList();
        }
        public Plant GetPlant(Guid Id) => _plantServices.GetPlant(Id);

        public void DeletePlant(Guid Id) => _plantServices.DeletePlant(Id);

        public void DeletePlant(Guid Id, Guid roomID)
        {
            var roomServices = DataAccessFactory.GetRoomServicesInstance();
            var room = roomServices.GetRoom(roomID);
            room.PlantsCount--;
            roomServices.UpdateRoom(room);
            _plantServices.DeletePlant(Id);
        }

        public void UpdatePlant(List<string> plantData, Guid plantID)
        {
            var plantToUpdate = _plantServices.GetPlant(plantID);
            FillData(plantToUpdate, plantData);
            try
            {
                _plantServices.UpdatePlant(plantToUpdate);
                _view.DisplayMessage("Successfuly updated plant");
            }
            catch (Exception)
            {
                _view.DisplayErrorMessage("An error has occured");
            }
        }

        public void UpdatePlant(Guid plantID)
        {
            var plantToUpdate = _plantServices.GetPlant(plantID);
            plantToUpdate.LastHydration = DateTime.Now;
            try
            {
                _plantServices.UpdatePlant(plantToUpdate);
                _view.DisplayMessage("Successfuly updated hydration date");
            }
            catch (Exception)
            {
                _view.DisplayErrorMessage("An error has occured");
            }
        }

        public void UpdateAllPlants(Guid roomID)
        {
            var plantsToUdate = _plantServices.GetRoomPlants(roomID);
            foreach(var plant in plantsToUdate)
            {
                plant.LastHydration = DateTime.Now;
                _plantServices.UpdatePlant(plant);
            }
            _view.DisplayMessage("Successfuly updated hydration date in all plants");
        }

        public void CreatePlant(List<string> plantData, Guid roomID)
        {
            var plantToCreate = DataAccessFactory.GetPlantInstance();
            var roomServices = DataAccessFactory.GetRoomServicesInstance();
            var room = roomServices.GetRoom(roomID);
            room.PlantsCount++;
            roomServices.UpdateRoom(room);
            plantToCreate.RoomID = roomID;
            FillData(plantToCreate, plantData);
            
            try
            {
                _plantServices.InsertPlant(plantToCreate);
                _view.DisplayMessage("Successfully added plant");
            }
            catch (Exception)
            {
                _view.DisplayErrorMessage("An error has occured");
            }
        }

        private void FillData(IPlant plant, List<string> plantData)
        {
            plant.PlantName = plantData[0];
            plant.PlantDescription = plantData[1];
            plant.HydrationNeeded = plantData[2];
            plant.IsSunNeeded = Boolean.Parse(plantData[3]);
            plant.ImageSource = plantData[4];
            SetDate(plant, plantData[5], plantData[6], plantData[7]);
        }

        private void SetDate(IPlant plant, string day, string month, string year)
        {
            try
            {
                var todayDate = DateTime.Now;
                var date = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                if (date > todayDate) throw new Exception();
                plant.LastHydration = date;
            }
            catch (Exception)
            {
                _view.DisplayErrorMessage("You provided wrong date");
                return;
            }
        }
    }
}
