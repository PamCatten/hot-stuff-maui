﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HotStuff.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Windows.Input;
using UraniumUI;

namespace HotStuff.Models;

public partial class ItemsPageViewModel : UraniumBindableObject
{
    // public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
    public List<Item> Items { get; set; } = new List<Item>();
    public List<Item> SelectedItems { get; set; } = new List<Item>();

    private Item newItem = new();
    public Item NewItem { get => newItem; set { newItem = value; OnPropertyChanged(); } }
    public ICommand AddNewItemCommand { get; protected set; }
    public ICommand GetItemsCommand { get; protected set; }
    public ICommand RemoveSelectedItemsCommand { get; protected set; }

    public ItemsPageViewModel()
    {

        async void AddItemFromDB()
        {
            Debug.WriteLine("Add items called.");
            await App.ItemServ.AddItem(NewItem);
        }

        AddNewItemCommand = new Command(() =>
        {
            Debug.WriteLine("User clicked add item.");
            AddItemFromDB();
            NewItem = new();
        });

        //GetItemsCommand = new Command(() =>
        //{
            //ObservableCollection<Item> items = new ObservableCollection<Item>();
            //Debug.WriteLine($"User clicked get items.");
            //items = GetItemsFromDB();
            //return new ObservableCollection<Item>(items);
        //});

         async void DeleteItemsFromDB(List<Item> Items)
        {
            Debug.WriteLine("Delete items called.");
            await App.ItemServ.DeleteItems(Items);
        }

        RemoveSelectedItemsCommand = new Command(async () =>
        {
            Debug.WriteLine("User clicked delete items.");
            DeleteItemsFromDB(SelectedItems);
        });

    }
}


