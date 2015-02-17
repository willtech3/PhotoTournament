using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoTournament.Controllers;
using PhotoTournament.Data;
using PhotoTournament.Model;

namespace PhotoTournament.Tests
{
    [TestClass]
    public class PicturesControllerTests
    {
        [TestMethod]
        public void PitcturesController_GetPhotoUrls_ShouldReturnListofStrings()
        {
            var controller = new PicturesController();
            var response = controller.GetPhotoUrls();
            response.Should().BeOfType<List<String>>();
        }

        [TestMethod]
        public void PicturesController_GetPhotoUrls_ShouldNotBeNull()
        {
            var controller = new PicturesController();
            var response = controller.GetPhotoUrls();
            response.Should().NotBeNull();
        }
    }

    [TestClass]
    public class WinnerControllerTests
    {
        [TestMethod]
        public void WinnerController_GetWinner_Should_Return_Url()
        {
            var fakeUoW = new Mock<PhotoTournamentUow>();
            fakeUoW.Setup(r => r.Winners.GetLatestWinnerByUsername(It.IsAny<String>()).CatPictureUrl).Returns("fakeUrl");
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("William", "Test"), new[]  {"fakeRole"});
            var controller = new WinnerController(fakeUoW.Object);
            var result = controller.GetWinner();

            result.Should().BeOfType<string>();
        }

        [TestMethod]
        public void WinnerController_GetAllWinnersTotalCount_ShouldBe_Ten()
        {   
            var fakeUoW = new Mock<PhotoTournamentUow>();
            fakeUoW.Setup(r => r.Winners).Returns(new FakeWinnerRepository(new Mock<DbContext>().Object));
            var controller = new WinnerController(fakeUoW.Object);
            var result = controller.GetAllWinners();

            result.TotalCount.Should().Be(10);
        }

        [TestMethod]
        public void WinnerController_GetAllWinnersTotalPages_ShouldBeOne()
        {
            var fakeUow = new Mock<PhotoTournamentUow>();
            fakeUow.Setup(r => r.Winners).Returns(new FakeWinnerRepository(new Mock<DbContext>().Object));
            var controller = new WinnerController(fakeUow.Object);

            var result = controller.GetAllWinners();

            result.TotalPages.Should().Be(1.0);
        }

        [TestMethod]
        public void WinnerController_SaveWinner_ShouldCallSaveWinner()
        {
            var fakeUow = new Mock<PhotoTournamentUow>();
            fakeUow.Setup(r => r.Winners).Returns(new FakeWinnerRepository(new Mock<DbContext>().Object));
            var controller = new Mock<WinnerController>(fakeUow.Object);
            controller.Object.SaveWinner(It.IsAny<WinnerController.UrlModel>());
            controller.Verify(r => r.SaveWinner(It.IsAny<WinnerController.UrlModel>()), Times.AtLeastOnce);
        }

        [ExpectedException(typeof(HttpResponseException))]
        [TestMethod]
        public void WinnerController_SaveWinner_ShouldThrowExceptionWhen_UrlModelIsEmptyString()
        {
            var fakeUow = new Mock<PhotoTournamentUow>();
            fakeUow.Setup(r => r.Winners).Returns(new FakeWinnerRepository(new Mock<DbContext>().Object));
            var controller = new WinnerController(fakeUow.Object);
            controller.SaveWinner(new WinnerController.UrlModel() {Url = string.Empty});
        }
    }
}