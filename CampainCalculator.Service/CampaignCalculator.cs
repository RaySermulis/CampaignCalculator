namespace CampainCalculator.Service
{
    public interface ICampaignCalculator
    {
        decimal CalculateComboCampaignPrice(IList<Product> comboItems, decimal comboPrice, IList<Product> checkoutBasketItems);
        decimal CalculateVolumeCampaignPrice(IList<Product> valumeCampaignItems, IList<Product> checkoutBasketItems, int minQuantity);
    }

    public class CampaignsCalculator : ICampaignCalculator
    {
        public decimal CalculateComboCampaignPrice(IList<Product> comboItems, decimal comboPrice, IList<Product> checkoutBasketItems)
        {
            if (!checkoutBasketItems.Any())
            {
                return 0;
            }

            //No items potential for campaign.
            if (checkoutBasketItems.Count == 1)
            {
                return checkoutBasketItems.First().Price;
            }

            var matchedBasketItems = new List<Product>();
            decimal regularPriceItemAmount = 0;

            //populate a list with items potential for campaign or add up regular price amount
            foreach (Product product in checkoutBasketItems)
            {
                if (comboItems.Select(x => x.EAN).Contains(product.EAN))
                {
                    matchedBasketItems.Add(product);
                }
                else
                {
                    regularPriceItemAmount += product.Price;
                }
            }

            //No items potential for campaign.
            if (matchedBasketItems.Count == 1)
            {
                return checkoutBasketItems.Sum(x => x.Price);
            }

            var campaignCombos = matchedBasketItems.Count / 2;
            var noComboItem = matchedBasketItems.Count % 2;

            var totalAmount = campaignCombos * comboPrice;
            if (noComboItem != 0)
            {
                //Adding the most expensive item from basket to total price for "single" item
                totalAmount += matchedBasketItems.OrderByDescending(x => x.Price).First().Price;
            }

            return totalAmount + regularPriceItemAmount;
        }

        public decimal CalculateVolumeCampaignPrice(IList<Product> valumeCampaignItems, IList<Product> checkoutBasketItems, int minQuantity)
        {
            if (!checkoutBasketItems.Any())
            {
                return 0;
            }

            //No items potential for campaign.
            if (checkoutBasketItems.Count == 1)
            {
                return checkoutBasketItems.First().Price;
            }

            var matchedBasketItems = new List<Product>();
            decimal regularPriceItemAmount = 0;

            //populate a list with items potential for campaign or add up regular price amount
            foreach (Product product in checkoutBasketItems)
            {
                if (valumeCampaignItems.Select(x => x.EAN).Contains(product.EAN))
                {
                    matchedBasketItems.Add(product);
                }
                else
                {
                    regularPriceItemAmount += product.Price;
                }
            }

            if (matchedBasketItems.Count <= 1)
            {
                return checkoutBasketItems.Sum(x => x.Price);
            }

            //grouping same items
            var groupedByEAN =
            from item in matchedBasketItems
            group item by item.EAN into newGroup
            orderby newGroup.Key
            select newGroup.ToList();

            decimal totalAmount = 0;

            //if the group qualifies for the minimum purchase quantity 
            //iterating through each group and adding up campaign price for each item 
            //and regular price for the remaining itmes
            foreach (var eangroup in groupedByEAN)
            {
                if (eangroup.Count >= minQuantity)
                {
                    var volumeCounter = 0;

                    foreach (var item in eangroup)
                    {
                        if (volumeCounter < minQuantity)
                        {
                            totalAmount += item.VolumeCampaignPrice;
                        }
                        else
                        {
                            totalAmount += item.Price;
                        }
                        volumeCounter++;
                    }
                }
                //regular price for groups under minimum purchase quantity 
                else
                {
                    foreach (var item in eangroup)
                    {
                        totalAmount += item.Price;
                    }
                }
            }

            return totalAmount + regularPriceItemAmount;
        }
    }
}