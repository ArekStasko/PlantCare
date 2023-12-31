﻿using PlantCare.DataAccess.models;

namespace PlantCare.DataAccess.services
{
    public interface IPlantServices
    {
        public List<Plant> GetPlants();
        public Plant GetPlant(Guid Id);
        public void InsertPlant(IPlant plant);
        public void DeletePlant(Guid Id);
        public void UpdatePlant(Plant plant);
        public List<Plant> GetRoomPlants(Guid RoomID);
    }
}
