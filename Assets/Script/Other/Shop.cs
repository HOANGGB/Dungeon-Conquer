using System.Collections.Generic;
using UnityEngine;

public class ItemShop
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public Sprite Image { get; set; } // Giả sử sử dụng Sprite để lưu trữ hình ảnh của vật phẩm

    public ItemShop(string name, string description, int price, Sprite image)
    {
        Name = name;
        Description = description;
        Price = price;
        Image = image;
    }
}

public class Shop : MonoBehaviour
{
     public List<ItemShop> itemShops = new List<ItemShop>();

    // Phương thức khởi tạo của script
    void Start()
    {
        // Thêm các mục vào danh sách khi script được khởi tạo
        AddItemsToShop();
    }

    // Phương thức để thêm các mục vào danh sách
   

    // Update is called once per frame
    void Update()
    {
        
    } 
    void AddItemsToShop()
    {
        itemShops.Add(new ItemShop("Sword", "A powerful sword for battle", 100,null));
        itemShops.Add(new ItemShop("Shield", "A sturdy shield for defense", 80,null));
        itemShops.Add(new ItemShop("Health Potion", "Restores health when consumed", 50,null));
    }

}
