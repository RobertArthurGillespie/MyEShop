using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEShop.Core.Contracts;
using MyEShop.Core.Models;
using MyEShop.Core.ViewModels;
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

            products.AddItem(new Product() { ID = "1", Price = 10.00m });
            products.AddItem(new Product() { ID = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            baskets.AddItem(basket);

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);

            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.ID });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketTotal);
        }
    }
}
