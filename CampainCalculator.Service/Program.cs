
using CampainCalculator.Service;

Console.WriteLine("Hello, World!");

var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637939", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "9999999999999", Price = 200, VolumeCampaignPrice = 15}
            };


var campaignItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637939", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7310865004703", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7340005404261", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7310532109090", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7611612222105", Price = 20, VolumeCampaignPrice = 15}
            };

Console.WriteLine($"Basket items:");
foreach (var item in basketItems)
{
    Console.WriteLine($"EAN: {item.EAN} Price: {item.Price}");
}

Console.WriteLine();
Console.WriteLine($"Campaign items:");
foreach (var item in campaignItems)
{
    Console.WriteLine($"EAN: {item.EAN} Price: {item.Price} VolumeCampaignPrice: {item.VolumeCampaignPrice}");
}

var calculator = new CampaignsCalculator();

Console.WriteLine();
Console.WriteLine($"Enter combo price.");
string input = Console.ReadLine();
decimal comboPrice = decimal.Parse(input);
Console.WriteLine($"Combo Campaign Total price: {calculator.CalculateComboCampaignPrice(campaignItems, comboPrice, basketItems)}");

Console.WriteLine();
Console.WriteLine($"Enter volume campaign minimum Quantity");
string minVolInput = Console.ReadLine();
int minQantity = int.Parse(minVolInput);
Console.WriteLine($"Volume Campaign total Price: {calculator.CalculateVolumeCampaignPrice(campaignItems, basketItems, minQantity)}");
