using EnergyTubo.Controllers;
using EnergyTubo.Interface;
using EnergyTubo.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboUnitTest
{

    public class LgaControllerTesting
    {
        private readonly LgasController lgasController;
        private readonly Mock<ILgaService> lgaService;
        public LgaControllerTesting()
        {
            lgaService = new Mock<ILgaService>();
            lgasController = new LgasController(lgaService.Object);
        }


        //[Fact]
        //public async void Get_Called_ReturnsOkResult()
        //{
        //    // Act
        //    var result = await lgasController.GetLGA();
        //    var okResult = result.Result as OkObjectResult;
        //    // Assert
        //    Assert.IsType<OkObjectResult>(okResult as ObjectResult);
        //}
        //[Fact]
        //public async void Get_Called_ReturnsAllItems()
        //{
        //    lgaService.Setup(repo => repo.GetLGAs())
        //     .ReturnsAsync(new List<LGA>() { new(), new(), new() });


        //    // Act

        //    var result = await LGAsController.Get();
        //    var okResult = result.Result as OkObjectResult;
        //    var output = Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        //    // Assert
        //    var myitems = Assert.IsType<List<LGA>>(output.Value);
        //    Assert.Equal(3, myitems.Count);
        //}


        [Fact]
        public void GetById_Existing_Id_Passed_ReturnsOkResult()
        {
            // Arrange
            var testId = new int();
            testId = 1;
            // Act
            var result = lgasController.GetLGA(testId);
            var okResult = result.Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result as OkObjectResult);
        }

        //[Fact]
        //public async void GetById_Existing_Id_Passed_ReturnsRigntItem()
        //{
        //    // Arrange

        //    var lgaList = GetLGAData();

        //    lgaService.Setup(repo => repo.GetLgaById(1))
        //     .ReturnsAsync(lgaList);



        //    var LGAResult = await lgasController.GetLGA(1);

        //    var okResult = LGAResult.Result as OkObjectResult;
        //    var output = Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        //    var item =Assert.IsType<List<LGA>>(output.Value);
        //    Assert.Equal(1, item.Count());

            
        //}


        private List<LGA> GetLGAData()
        {
            List<LGA> LGAsData = new List<LGA>
        {
            new LGA
            {
                Id = 1,
                Name = "Kano",
                StateId = 1,

            },
             new LGA
            {
                Id = 2,
                Name = "Oyo",
                StateId = 1,

            },
             new LGA
            {
                Id = 3,
                Name = "Ondo",
                StateId = 2,

            },
        };
            return LGAsData;
        }

    }
}
