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
    public class UnitTestingTurbo
        

       {
        private readonly StatesController statesController;
        private readonly Mock<IStateService> stateService;
        public UnitTestingTurbo()
        {
            stateService = new Mock<IStateService>();
            statesController = new StatesController(stateService.Object);
        }


        [Fact]
        public async void Get_Called_ReturnsOkResult()
        {
            // Act
            var result = await statesController.Get();
            var okResult = result.Result as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(okResult as ObjectResult);
        }
        [Fact]
        public async void Get_Called_ReturnsAllItems()
        {
            stateService.Setup( repo => repo.GetStates())
             .ReturnsAsync( new List<State>() { new(), new(),new() });
        

            // Act

            var result = await statesController.Get();
            var okResult = result.Result as OkObjectResult;
            var output = Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
            // Assert
            var myitems = Assert.IsType<List<State>>(output.Value);
            Assert.Equal(3, myitems.Count);
        }


        [Fact]
        public void GetById_Existing_Id_Passed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new int();
            testGuid = 1;
            // Act
            var result = statesController.GetStateById(testGuid);
            var okResult = result.Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result as OkObjectResult);
        }

        [Fact]
        public async void GetById_Existing_Id_Passed_ReturnsRigntItem()
        {
            // Arrange
            
            var stateList = GetStateData();

            stateService.Setup(repo => repo.GetStateById(1))
             .ReturnsAsync(stateList[0]);


             
            var stateResult = await statesController.GetStateById(1);

            var okResult = stateResult.Result as OkObjectResult;
            var output = Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
            Assert.IsType<State>(output.Value);
            Assert.Equal(1, (output.Value as State).Id);

           
           




          
        }


        private List<State> GetStateData()
        {
            List<State> StatesData = new List<State>
        {
            new State
            {
                Id = 1,
                Name = "Kano",
                
            },
             new State
            {
                Id = 2,
                Name = "Oyo",
                
            },
             new State
            {
                Id = 3,
                Name = "Ondo",
                
            },
        };
            return StatesData;
        }

    }
}
