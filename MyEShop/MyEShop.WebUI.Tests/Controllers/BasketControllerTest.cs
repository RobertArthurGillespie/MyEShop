using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEShop.Core.Contracts;
using MyEShop.Core.Models;
using MyEShop.Services;
using MyEShop.WebUI.Controllers;
using MyEShop.WebUI.Tests.Mocks;

namespace MyEShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);
            
            //Act
            
            //basketService.AddToBasket(httpContext, "1");

            controller.AddToBasket("1");
            

            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            var httpContext = new MockHttpContext();
        }
    }
}
