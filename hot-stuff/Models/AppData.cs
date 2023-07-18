namespace HotStuff.Models
{
    public static class AppData
    {
        public static List<Item> Items = new List<Item>
        {
            new Item()
            {
                ItemName = "Pride and Prejudice", Category = ItemCategory.BooksMagazines, BrandManufacturer = "Bantam Classics",
                Room = ItemRoom.Library, ItemVersion = "Perma-Bound Hardcover", Color = ItemColor.Black, PurchaseDate = "09/02/2017",
                AmountPaid = 12.99m, ItemDescription = "Written by Jane Austen", PurchaseProof = "https://www.aws.com/exampleurl/"
            },
            new Item()
            {
                ItemName = "Aug 2023 VOGUE Magazine", Category = ItemCategory.BooksMagazines, BrandManufacturer = "Vogue",
                Room = ItemRoom.Library, ItemVersion = "Aug 2023 Issue", Color = ItemColor.White, PurchaseDate = "07/17/2023",
                AmountPaid = 3.99m, ItemDescription = "August 2023 Issue, Olivia Rodrigo cover-model", PurchaseProof = "https://www.aws.com/exampleurl/"
            }

        };
    }
}
