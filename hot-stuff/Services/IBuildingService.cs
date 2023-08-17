namespace HotStuff.Services
{
    public interface IBuildingService
    {
        Task<ObservableCollection<Building>> GetBuilding();
        Task AddBuilding(Building building);
        Task UpdateBuilding(Building building);
        Task DeleteBuilding(Building building);
        Task FlushBuildings();
    }
}
