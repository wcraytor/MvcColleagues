﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcColleagues.Controllers;
using MvcColleagues.Models.SiteMembers;
using MvcColleagues.Repositories;

namespace MvcColleagues.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private static Mock<SiteMemberRepository> SiteMemberRepositoryMock;
        private static Mock<IRepository<Colleague>> ColleagueRepositoryMock;

        [ClassInitialize]
        public static void HomeControllerTestInitialize(TestContext context)
        {
            SiteMemberRepositoryMock = new Mock<SiteMemberRepository>();
            ColleagueRepositoryMock = new Mock<IRepository<Colleague>>();
        }

        [ClassCleanup]
        public static void HomeControllerTestCleanup()
        {
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = GetHomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            //Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);


            var members = new List<SiteMember>
                {
                    new SiteMember()
                };

            SiteMemberRepositoryMock.Setup(s => s.GetAll())
                                    .Returns(members.AsQueryable())
                                    .Verifiable();

            // Act
            ActionResult result2 = controller.Index();

            // Assert
            //SiteMemberRepositoryMock.Verify();

            Assert.IsInstanceOfType(result2, typeof (ViewResult));

            var view = (ViewResult) result2;

            Assert.IsInstanceOfType(view.Model, typeof (HandleErrorInfo));
        }

        private HomeController GetHomeController()
        {
            return new HomeController(SiteMemberRepositoryMock.Object, ColleagueRepositoryMock.Object);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = GetHomeController();

            // Act
            var result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = GetHomeController();

            // Act
            var result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}