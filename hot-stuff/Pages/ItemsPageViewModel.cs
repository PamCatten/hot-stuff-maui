using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UraniumUI;

namespace hot_stuff.Pages
{
    internal class ItemsPageViewModel : BindableObject
    {
        public ObservableCollection<Item> Items { get; protected set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> SelectedItems { get; set; } = new ObservableCollection<Item>();

        private Item newItemPrivate = new();
        public Item newItemPublic { get => newItemPrivate; set { newItemPublic = value; OnPropertyChanged(); } }

        public ICommand AddNewItemCommand { get; set; }
        public ICommand RemoveSelectedItemCommand { get; protected set; }
        public ItemsPageViewModel() 
        {
            if (Items.Count == 0)
            {
                Items.Add(new Item { Content = "Cartier Bracelet", Category = Item.ItemCategory.Jewelry });
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
        public string Content { get; set; }

        public ItemCategory Category { get; set; }
        public static ItemCategory[] AvailableCategories => Enum.GetValues(typeof(ItemCategory)) as ItemCategory[];
        public enum ItemCategory
        {
            Appliances,
            Books,
            Clothing,
            Electronics,
            Jewelry,
            Shoes,
        }
    }

}

