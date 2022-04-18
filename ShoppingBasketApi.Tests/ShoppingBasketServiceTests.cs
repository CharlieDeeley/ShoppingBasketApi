using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingBasketApi.Models;
using ShoppingBasketApi.Objects;
using ShoppingBasketApi.Services.Abstract;
using ShoppingBasketApi.Services.Concrete;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasketApi.Tests
{
    [TestClass]
    public class ShoppingBasketServiceTests
    {
        private IShoppingBasketService _shoppingBasketService;
        private IShoppingBasketRepository _shoppingBasketRepository;
        private Mock<IPriceConverterService> _priceConvertService;

        private IShoppingBasketRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<ShoppingBasketContext>()
                .UseInMemoryDatabase(databaseName: "MockDb", new InMemoryDatabaseRoot())
                .Options;

            var context = new ShoppingBasketContext(options);

            var repository = new DbRepository(context);

            return repository;
        }

        [TestInitialize]
        public void Setup()
        {
            // setup mock price converter with mock return to prevent real api calls
            // 2 for simple maths in tests 
            _priceConvertService = new Mock<IPriceConverterService>();
            _priceConvertService.Setup(x => x.GetConversionRate(It.IsAny<string>())).Returns(Task.FromResult(2d));

            _shoppingBasketRepository = GetInMemoryRepository();
            _shoppingBasketService = new ShoppingBasketService(_priceConvertService.Object, _shoppingBasketRepository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _shoppingBasketRepository = null;
        }

        [TestMethod]
        public void AddItem_ItemAdded_CorrectItemAdded()
        {
            _shoppingBasketService.AddItem(0);

            var itemInDb = _shoppingBasketRepository.Items.First(x => x.Name == "Soup");

            Assert.AreEqual(1, _shoppingBasketRepository.Items.Count());

            Assert.AreEqual("Soup", itemInDb.Name);
            Assert.AreEqual(0.65m, itemInDb.Price);
        }

        [TestMethod]
        public void AddItem_ItemAlreadyInBasket_Throws()
        {
            _shoppingBasketService.AddItem(0);

            var ex = Assert.ThrowsException<ArgumentException>(() => _shoppingBasketService.AddItem(0));

            Assert.AreEqual(ex.Message, "Item is already in basket");
        }

        [TestMethod]
        public void AddItem_ItemNotStocked_Throws()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => _shoppingBasketService.AddItem(10));

            Assert.AreEqual(ex.Message, "Item not in stock! Please check available items and try again.");
        }

        [TestMethod]
        public void RemoveItem_ItemRemoved_ItemSuccessfullyRemoved()
        {
            _shoppingBasketService.AddItem(1);

            _shoppingBasketService.RemoveItem(1);
            Assert.AreEqual(0, _shoppingBasketRepository.Items.Count());
        }

        [TestMethod]
        public void RemoveItem_NoItems_Throws()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => _shoppingBasketService.RemoveItem(1));

            Assert.AreEqual(ex.Message, "There are no items in basket to remove");
        }

        [TestMethod]
        public void RemoveItem_ItemNotInBasket_Throws()
        {
            _shoppingBasketService.AddItem(0);
            var ex = Assert.ThrowsException<ArgumentException>(() => _shoppingBasketService.RemoveItem(2));

            Assert.AreEqual(ex.Message, "Item not in basket");
        }

        [TestMethod]
        public void GetAvailableItems_GetsAvailableItems()
        {
            var items = _shoppingBasketService.GetAvailableItems(It.IsAny<string>());

            Assert.AreEqual(4, items.Result.Count());
        }

        [TestMethod]
        public void GetAvailableItems_PricesConvertedCorrectly()
        {
            var items = _shoppingBasketService.GetAvailableItems(It.IsAny<string>());

            Assert.AreEqual(1.3m, items.Result.ToArray()[0].Price);
            Assert.AreEqual(1.6m, items.Result.ToArray()[1].Price);
            Assert.AreEqual(2.3m, items.Result.ToArray()[2].Price);
            Assert.AreEqual(2m, items.Result.ToArray()[3].Price);
        }

        [TestMethod]
        public void GetBasket_ItemsInBasket_ReturnsItems()
        {
            _shoppingBasketService.AddItem(0);
            var itemInDb = _shoppingBasketRepository.Items.First(x => x.Name == "Soup");

            var items = _shoppingBasketService.GetBasket(It.IsAny<string>());


            Assert.AreEqual(1, items.Result.Count());
            Assert.AreEqual(itemInDb.Name, items.Result.ToArray()[0].Name);
        }

        [TestMethod]
        public void GetBasket_ItemsInBasket_PricesCorrectlyConverted()
        {
            _shoppingBasketService.AddItem(0);

            var items = _shoppingBasketService.GetBasket(It.IsAny<string>());

            Assert.AreEqual(1.3m, items.Result.ToArray()[0].Price);
        }
    }
}