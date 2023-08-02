using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HotStuff.Model;

[Table("Items")]
public partial class Item : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    [PrimaryKey, AutoIncrement]
    public int ItemID { get; set; }

    private string itemName;
    private string brandManufacturer;
    private string itemDescription;
    private string purchaseProof;
    private string dateAcquired;
    private decimal itemPrice;
    private int itemQuantity;
    
    public string ItemName
    {
        get
        {
            return itemName;
        }
        set
        {
            if (itemName != value)
            {
                itemName = value;
            }
            OnPropertyChanged();
        }
    }
    public string BrandManufacturer
    {
        get
        {
            return brandManufacturer;
        }
        set
        {
            if (brandManufacturer != value)
            {
                brandManufacturer = value;
            }
            OnPropertyChanged();
        }
    }
    public string ItemDescription
    {
        get
        {
            return itemDescription;
        }
        set
        {
            if (itemDescription != value)
            {
                itemDescription = value;
            }
            OnPropertyChanged();
        }
    }
    public string PurchaseProof
    {
        get
        {
            return purchaseProof;
        }
        set
        {
            if (purchaseProof != value)
            {
                purchaseProof = value;
            }
            OnPropertyChanged();
        }
    }
    public string DateAcquired
    {
        get
        {
            return dateAcquired;
        }
        set
        {
            if (dateAcquired != value)
            {
                dateAcquired = value;
            }
            OnPropertyChanged();
        }
    }
    public decimal ItemPrice
    {
        get
        {
            return itemPrice;
        }
        set
        {
            if (itemPrice != value)
            { 
                itemPrice = value; 
            }
            OnPropertyChanged();
        }
    }
    public int ItemQuantity
    {
        get 
        { 
            return itemQuantity; 
        }
        set 
        { 
            if (itemQuantity != value) 
            {
                itemQuantity = value; 
            } 
            OnPropertyChanged(); 
        }
    }


    public ItemCategory? Category { get; set; }

    public ItemRoom? Room { get; set; }

    public ItemColor? Color { get; set; }

    public static ItemCategory[] AvailableCategories => Enum.GetValues(typeof(ItemCategory)) as ItemCategory[];
    public static ItemRoom[] AvailableRooms => Enum.GetValues(typeof(ItemRoom)) as ItemRoom[];
    public static ItemColor[] AvailableColors => Enum.GetValues(typeof(ItemColor)) as ItemColor[];
}
