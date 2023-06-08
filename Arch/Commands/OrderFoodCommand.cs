﻿using System.Runtime.InteropServices.ComTypes;
using ArchProject.Enums;
using ArchProject.Models;
using ArchProject.Services;

namespace ArchProject.Commands;

public class OrderFoodCommand : ICommand
{
    private readonly IStoreService _storeService;
    private readonly ICartEntryService _cartEntryService;
    private readonly IStoreFoodItemService _storeFoodItemService;
    private readonly IOrderService _orderService;

    public OrderFoodCommand(IStoreService storeService, IOrderService orderService, ICartEntryService cartEntryService, IStoreFoodItemService storeFoodItemService)
    {
        _storeService = storeService;
        _cartEntryService = cartEntryService;
        _storeFoodItemService = storeFoodItemService;
        _orderService = orderService;
    }

    public void Execute()
    {
        var store = SelectStore();
        if (store == null)
        {
            return;
        }

        var selectFoodItemsOfStore = SelectStoreFoodItems(store.Id);
        _cartEntryService.AddToCart(selectFoodItemsOfStore);
        
        
        Console.WriteLine("Do you want to pay now? (y/n)");
        var input = Console.ReadLine();
        switch (input)
        {
            case "n" or "N":
                return;
            case "y" or "Y":
                OrderFood();
                break;
        }
    }

    private void OrderFood()
    {
        var order = new Order
        {
            Status = OrderStatus.Paid
        };
        _orderService.AddOrder(order);
        var orderStoreFoodItems = _cartEntryService.GetCart().Select(cartEntry => new OrderStoreFoodItem
        {
            OrderId = order.Id,
            StoreId = cartEntry.StoreId,
            FoodItemId = cartEntry.FoodItemId,
            Quantity = cartEntry.Quantity
        }).ToList();
        _orderService.AddOrderStoreFoodItems(orderStoreFoodItems);
    }

    private Store? SelectStore()
    {
        Console.WriteLine("Available Stores:");
        var stores = _storeService.GetAllStores();
        foreach (var store in stores)
        {
            Console.WriteLine($"Store id: {store.Id}, Name: {store.Name}, Opening hours: {store.OpeningTime.ToString(@"hh\:mm")} - {store.ClosingTime.ToString(@"hh\:mm")}");
        }
        
        Console.Write("Please enter the id of the store you want to order from:");
        var input = Console.ReadLine();
        var inputId = int.Parse(input ?? string.Empty);
        if (stores.Any(s => s.Id == inputId))
        {
            var store = stores.First(s => s.Id == inputId);
            if (store.IsOpen() == false)
            {
                Console.WriteLine("Store is closed!");
            }
            return store;
        }
        Console.WriteLine("Invalid store id!");
        return null;
    }

    private List<StoreFoodItem> SelectStoreFoodItems(int storeId)
    {
        var selectedStoreFoodItems = new List<StoreFoodItem>();
        var storeFoodItems = _storeFoodItemService.GetAllStoreFoodItemsByStoreId(storeId);
        if (storeFoodItems.Count == 0)
        {
            Console.WriteLine("No food items available in this store!");
            return selectedStoreFoodItems;
        }
        while (true)
        {
            foreach (var storeFoodItem in storeFoodItems)
            {
                Console.WriteLine(
                    $"Food id: {storeFoodItems.IndexOf(storeFoodItem)}, Name: {storeFoodItem.FoodItem.Name}, Price: {storeFoodItem.Price}");
            }

            Console.Write("Please enter the id of the food you want to order or type 'q' to end selection:");
            var input = Console.ReadLine();
            if (input is "q" or "Q")
            {
                return selectedStoreFoodItems;
            }
            
            var id = int.Parse(input ?? string.Empty);
            if (id < storeFoodItems.Count && id >= 0)
            {
                var storeFoodItem = storeFoodItems[id];
                selectedStoreFoodItems.Add(storeFoodItem);
                Console.WriteLine("Food added to cart!");
                continue;
            }
            Console.WriteLine("Invalid food id!"); 
        }
    }
    
    
    
    public void Undo()
    {
        // idk how to undo a complex command like this
        // if there was payment made then undo payment
        // if there were foods selected then unselect them
    }

    public string Description => "Order food";
}