using CampainCalculator.Service;
using Xunit;

namespace CampaignCalculator.Serivice.Tests
{
    public class CampaignCalculatorTests
    {
        [Fact]
        public void CalculateComboCampaign_one_combo_and_regular()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20},
                new Product {EAN = "5000112637939",Price = 20},
                new Product {EAN = "9999999999999",Price = 100}
            };

            var expectedTotalAmount = 130;

            //Act
            var result = comboCalculator.CalculateComboCampaignPrice(CreateCampaignProducts(), 30, basketItems);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact]
        public void CalculateComboCampaign_one_combo_one_regular_and_matchNoCombo()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20},
                new Product {EAN = "5000112637939",Price = 20},
                new Product {EAN = "7310865004703",Price = 20},
                new Product {EAN = "9999999999999",Price = 100}
            };

            var expectedTotalAmount = 150;

            //Act
            var result = comboCalculator.CalculateComboCampaignPrice(CreateCampaignProducts(), 30, basketItems);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact]
        public void CalculateComboCampaign_matchNoCombo_noRegular()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20}
            };

            var expectedTotalAmount = 20;

            //Act
            var result = comboCalculator.CalculateComboCampaignPrice(CreateCampaignProducts(), 30, basketItems);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact]
        public void CalculateComboCampaign_multiCombo_and_regular()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637939", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "7310865004703", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "7340005404261", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "7310532109090", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "7611612222105", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "9999999999999", Price = 200, VolumeCampaignPrice = 15}
            };

            var expectedTotalAmount = 290;

            //Act
            var result = comboCalculator.CalculateComboCampaignPrice(CreateCampaignProducts(), 30, basketItems);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact]
        public void CalculateValumeCampaign_with_campaign_price_activated_and_not_activated_and_noMatch()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637922", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637939", Price = 20 , VolumeCampaignPrice = 15},
                new Product {EAN = "9999999999999", Price = 200, VolumeCampaignPrice = 15}
            };

            var expectedTotalAmount = 265;

            //Act
            var result = comboCalculator.CalculateVolumeCampaignPrice(CreateCampaignProducts(), basketItems, 3);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact]
        public void CalculateValumeCampaign_campaign_not_activated_and_noMatch()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "9999999999999", Price = 50, VolumeCampaignPrice = 35}
            };

            var expectedTotalAmount = 70;

            //Act
            var result = comboCalculator.CalculateVolumeCampaignPrice(CreateCampaignProducts(), basketItems, 2);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        [Fact]
        public void CalculateValumeCampaign_campaign_not_reaching_required_quantity()
        {
            //Arrange
            var comboCalculator = new CampaignsCalculator();
            var basketItems = new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637922", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637922", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "9999999999999", Price = 50, VolumeCampaignPrice = 35}
            };

            var expectedTotalAmount = 110;

            //Act
            var result = comboCalculator.CalculateVolumeCampaignPrice(CreateCampaignProducts(), basketItems, 4);

            //Assert
            Assert.Equal(expectedTotalAmount, result);
        }

        private IList<Product> CreateCampaignProducts()
        {
            return new List<Product>
            {
                new Product {EAN = "5000112637922", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "5000112637939", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7310865004703", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7340005404261", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7310532109090", Price = 20, VolumeCampaignPrice = 15},
                new Product {EAN = "7611612222105", Price = 20, VolumeCampaignPrice = 15}
            };
        }
    }
}