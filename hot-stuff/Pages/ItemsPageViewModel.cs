using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Pages
{
    internal class ItemsPageViewModel : BindableObject
    {
        public ObservableCollection<Item> Items { get; protected set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();

        private Item newItemPrivate = new();
        public Item newItemPublic { get => newItemPrivate; set { newItemPrivate = value; OnPropertyChanged(); } }

        public ICommand AddNewItemCommand { get; set; }
        public ICommand RemoveSelectedItemCommand { get; protected set; }
        public ItemsPageViewModel() 
        {
            if (Items.Count == 0)
            {
                Items.Add(new Item 
                { 
                    ItemName = "Pride and Prejudice", 
                    Category = Item.ItemCategory.BooksMagazines, 
                    BrandManufacturer = "Bantam Classics", 
                    Room = Item.LocationRoom.Library,
                    ItemVersion = "Perma-Bound Hardcover",
                    Color = Item.ItemColor.Black,
                    PurchaseDate = "08/02/2017",
                    AmountPaid = 12.99m,
                    PurchaseProof = "https://www.aws.com/example/ghshund80dsfmmdae/",
                    ItemDescription = "Pride and Prejudice, the novel written by Jane Austen",
                });
            }

            AddNewItemCommand = new Command(() =>
            {
                Items.Insert(0, newItemPublic);
                newItemPublic = new();
            });

            RemoveSelectedItemCommand = new Command(() =>
            {
                foreach (var item in SelectedItems)
                {
                    Items.Remove(item);
                }
                
            });
        }
    }

    public class Item : UraniumBindableObject
    {
        public string ItemName { get; set; }
        public ItemCategory Category { get; set; } // TODO: another drop down menu here
        public static ItemCategory[] AvailableCategories => Enum.GetValues(typeof(ItemCategory)) as ItemCategory[];
        public enum ItemCategory
        {
            Antiques,
            Apparel,
            ApparelAccessories,
            ApparelBags,
            ApparelJewelry,
            Appliances,
            Artwork,
            Baby,
            Beauty,
            BooksMagazines,
            Collectibles,
            CraftSupplies,
            Electronics,
            Fixtures,
            Furniture,
            Games,
            Garden,
            Grocery,
            Handmade,
            HomeDecor,
            HomeImprovementSupplies,
            KitchenUtensils,
            Lighting,
            Linens,
            MusicalInstruments,
            OfficeSupplies,
            PersonalCare,
            PetSupplies,
            PlumbingHVAC,
            Shoes,
            SportsEquipment,
            Storage,
            Tools,
            Toys,
            Vehicles,
            VehicleSupplies,
            Other,
        }
        public LocationRoom Room { get; set; } // TODO: another drop down menu here
        public static LocationRoom[] AvailableRooms => Enum.GetValues(typeof(LocationRoom)) as LocationRoom[];
        public enum LocationRoom
        {
            Attic,
            Barn,
            Basement,
            Bathroom,
            Bedroom,
            Closet,
            Conservatory,
            Crawlspace,
            Den,
            DiningRoom,
            Driveway,
            Entryway,
            FamilyRoom,
            Garage,
            Garden,
            Greenhouse,
            GuestBedroom,
            GuestHouse,
            Gym,
            Hallway,
            KidsBedroom,
            Kitchen,
            LaundryRoom,
            Library,
            LivingRoom,
            Loft,
            MasterBathroom,
            MasterBedroom,
            Mudroom,
            Nursery,
            Office,
            Pantry,
            Patio,
            Playroom,
            Porch,
            Shed,
            Storage,
            Studio,
            Sunroom,
            UtilityRoom,
            WalkInCloset,
            Workshop,
            Other,
        }
        public string BrandManufacturer { get; set; }
        public string ItemVersion { get; set; }
        public ItemColor Color { get; set; }
        public static ItemColor[] AvailableColors => Enum.GetValues(typeof(ItemColor)) as ItemColor[];
        public enum ItemColor // TODO: Ideally this is a dropdown menu
        {
            White,
            Black,
            Grey,
            Brown,
            Gold,
            Silver,
            Pink,
            Red,
            Orange,
            Yellow,
            Green,
            Aqua,
            Blue,
            Purple,
            Magenta,
            Transparent,
            Other,
        }
        public string PurchaseDate { get; set; } // TODO: finish functionality, needs to be in date format, should probabaly use a date picker here
        public decimal AmountPaid { get; set; } // I don't know if this is necessary, but it would allow us to create all types of charts from it
        public string PurchaseProof { get; set; } // TODO: finish functionality, should be an image, or a link to an image stored in the cloud
        public string ItemDescription { get; set; }

    }

}

