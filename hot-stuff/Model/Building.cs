using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HotStuff.Model;

[Table("Buildings")]
public partial class Building : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    [PrimaryKey, AutoIncrement]
    public int BuildingID { get; set; }

    private string buildingName;
    private string buildingDescription;
    private int buildingItemCount;
    private decimal buildingValue;
    private ObservableCollection<Item> buildingManifest;
    private Dictionary<ItemRoom, decimal> buildingRoomValue;
    private Dictionary<ItemCategory, int> buildingCategoryCount;

    public string BuildingName
    {
        get
        {
            return buildingName;
        }
        set
        {
            if (buildingName != value)
            {
                buildingName = value;
            }
            OnPropertyChanged();
        }
    }
    public string BuildingDescription
    {
        get
        {
            return buildingDescription;
        }
        set
        {
            if (buildingDescription != value)
            {
                buildingDescription = value;
            }
            OnPropertyChanged();
        }
    }
    public int BuildingItemCount
    {
        get
        {
            return BuildingManifest.Count;
        }
        set
        {
            if (buildingItemCount != value)
            {
                buildingItemCount = BuildingManifest.Count;
            }
            OnPropertyChanged();
        }
    }
    public decimal BuildingValue
    {
        get
        {
            return buildingValue;
        }
        set
        {
            if (buildingValue != value)
            {
                buildingValue = value;
            }
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Item> BuildingManifest
    {
        get
        {
            return buildingManifest;
        }
        set
        {
            if (buildingManifest != value)
            {
                buildingManifest = value;
            }
            OnPropertyChanged(nameof(buildingManifest));
        }
    }

    public Dictionary<ItemRoom, decimal> BuildingRoomValue
    {
        get
        {
            return buildingRoomValue;
        }
        set
        {
            if (buildingRoomValue != value)
            {
                buildingRoomValue = value;
            }
            OnPropertyChanged(nameof(buildingRoomValue));
        }
    }

    public Dictionary<ItemCategory, int> BuildingCategoryCount
    {
        get
        {
            return buildingCategoryCount;
        }
        set
        {
            if (buildingCategoryCount != value)
            {
                buildingCategoryCount = value;
            }
            OnPropertyChanged(nameof(buildingCategoryCount));
        }
    }

    public BuildingType? BuildingType { get; set; }

    public static BuildingType[] AvailableCategories => Enum.GetValues(typeof(BuildingType)) as BuildingType[];
}
