using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Xunit;
using Inventorify.Models;

namespace Inventorify.Api
{
    public class InventoryItemsTestController : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public InventoryItemsTestController(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetInventoryItems()
        {
            // Act
            var httpResponse = await _client.GetAsync("/api/inventoryItems");
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var inventoryItem = JsonConvert.DeserializeObject<IEnumerable<Models.InventoryItem>>(stringResponse);
            Assert.Contains(inventoryItem, i => i.Name == "Bottled water");
            Assert.Contains(inventoryItem, i => i.Name == "Socks");
            Assert.Contains(inventoryItem, i => i.Name == "Coffee machine");
            Assert.Contains(inventoryItem, i => i.Name == "Hand sanitizer");
            Assert.Contains(inventoryItem, i => i.Name == "A4 paper");
            Assert.Contains(inventoryItem, i => i.Name == "Table lamp");
            Assert.Contains(inventoryItem, i => i.Name == "Tape");
        }

        [Fact]
        public async Task GetInventoryItem3()
        {
            // Act
            var httpResponse = await _client.GetAsync("/api/inventoryItems/3");
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(stringResponse);
            Assert.Equal(3, inventoryItem.Id);
            Assert.Equal("Coffee machine", inventoryItem.Name);
            Assert.Equal("Appliances", inventoryItem.Group);
            Assert.Equal(18, inventoryItem.Count);
            Assert.Equal((float)28.49, inventoryItem.UnitPrice);
            Assert.Equal(512.82, inventoryItem.TotalPrice);
        }

        [Fact]
        public async Task PutInventoryItem6()
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(new {id=6, name="Pillow", group="Home", count=750, unitPrice=(float)6.50 });
            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Act
            var httpResponse = await _client.PutAsync("/api/inventoryItems/"+6, httpContent);
            //httpResponse.EnsureSuccessStatusCode();
            var checkHttpResponse = await _client.GetAsync("/api/inventoryItems/"+6);

            // Assert
            var stringResponse = await checkHttpResponse.Content.ReadAsStringAsync();
            var inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(stringResponse);
            Assert.Equal(6, inventoryItem.Id);
            Assert.Equal("Pillow", inventoryItem.Name);
            Assert.Equal("Home", inventoryItem.Group);
            Assert.Equal(750, inventoryItem.Count);
            Assert.Equal((float)6.50, inventoryItem.UnitPrice);
            Assert.Equal(4875, inventoryItem.TotalPrice);
        }

        [Fact]
        public async Task PutInventoryItemNoId()
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(new {name = "Tape", group = "Office Supplies", count = 280, unitPrice = (float)4.25 });
            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Act
            var httpResponse = await _client.PutAsync("/api/inventoryItems/" + 3, httpContent);

            // Assert
            Assert.False(httpResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task PostInventoryItem()
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(new { name = "Swedish fish", group = "Food & Beverages", count = 50, unitPrice = (float)2.50 });
            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Act
            var httpResponse = await _client.PostAsync("/api/inventoryItems", httpContent);
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(stringResponse);
            int Id = inventoryItem.Id;
            Assert.Equal("Swedish fish", inventoryItem.Name);
            Assert.Equal("Food & Beverages", inventoryItem.Group);
            Assert.Equal(50, inventoryItem.Count);
            Assert.Equal((float)2.50, inventoryItem.UnitPrice);
            Assert.Equal(125, inventoryItem.TotalPrice);
        }

        [Fact]
        public async Task PostInventoryItem_ThenDeleteInventoryItem()
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(new { name = "Swedish fish", group = "Food & Beverages", count = 50, unitPrice = (float)2.50 });
            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            // Act
            var httpResponse = await _client.PostAsync("/api/inventoryItems", httpContent);
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(stringResponse);
            int Id = inventoryItem.Id;
            Assert.Equal("Swedish fish", inventoryItem.Name);
            Assert.Equal("Food & Beverages", inventoryItem.Group);
            Assert.Equal(50, inventoryItem.Count);
            Assert.Equal((float)2.50, inventoryItem.UnitPrice);
            Assert.Equal(125, inventoryItem.TotalPrice);

            // Act
            httpResponse = await _client.DeleteAsync("/api/inventoryItems/"+Id);
            httpResponse.EnsureSuccessStatusCode();

            // Assert
            stringResponse = await httpResponse.Content.ReadAsStringAsync();
            inventoryItem = JsonConvert.DeserializeObject<InventoryItem>(stringResponse);
            Assert.Equal(Id, inventoryItem.Id);
            Assert.Equal("Swedish fish", inventoryItem.Name);
            Assert.Equal("Food & Beverages", inventoryItem.Group);
            Assert.Equal(50, inventoryItem.Count);
            Assert.Equal((float)2.50, inventoryItem.UnitPrice);
            Assert.Equal(125, inventoryItem.TotalPrice);
        }

        [Fact]
        public async Task DeleteNoId()
        {
            // Act
            var httpResponse = await _client.DeleteAsync("/api/inventoryItems");

            // Assert
            Assert.False(httpResponse.IsSuccessStatusCode);
        }
    }
}
