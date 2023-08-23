﻿using SQLite;
namespace HotStuff.Services;
public class BuildingService : IBuildingService
{
    private readonly string buildingDatabasePath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection BuildingDatabase;

    async Task Init()
    {

        if (BuildingDatabase is not null)
            return;

        BuildingDatabase = new SQLiteAsyncConnection(buildingDatabasePath);

        await BuildingDatabase.CreateTableAsync<Building>();

    }

    public BuildingService(string BuildingDatabasePath)
    {
        buildingDatabasePath = BuildingDatabasePath;
    }

    public async Task AddBuilding(Building building)
    {
        try
        {
            await Init();

            await BuildingDatabase.InsertAsync(building);

            Debug.WriteLine($"----Record saved. Added: {building.BuildingID}, {building.BuildingName}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"----Failed to add {building.BuildingID}, {building.BuildingName}. Error: {ex.Message}");
        }
    }

    public async Task<ObservableCollection<Building>> GetBuildings()
    {
        try 
        {
            await Init();
            Debug.WriteLine("Data retrieval attempted.");
            return new ObservableCollection<Building>(await BuildingDatabase.Table<Building>().ToListAsync());
        }
        catch (Exception ex) 
        {
            Debug.WriteLine($"Failed to retrieve data. {ex.Message}");
        }

        return new ObservableCollection<Building>();
    }

    public async Task DeleteBuilding(Building building)
    {
        await Init();
        await BuildingDatabase.DeleteAsync<Building>(building.BuildingID);
    }

    public async Task UpdateBuilding(Building building)
    {
        try
        {
            await Init();
            await BuildingDatabase.UpdateAsync(building);
            Debug.WriteLine($"----Record saved. Updated: {building.BuildingID}, {building.BuildingName}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"----Failed to update {building.BuildingID}, {building.BuildingName}. Error: {ex.Message}");
        }
    }

    public async Task FlushBuildings()
    {
        await Init();
        Debug.WriteLine("Emergency building flush started.");
        await BuildingDatabase.DeleteAllAsync<Building>();
        Debug.WriteLine("Emergency building flush finished.");
    }
}
