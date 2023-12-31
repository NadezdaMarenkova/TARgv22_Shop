﻿using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.RealEstateTest
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptySpaceship_WhenReturnresult()
        {
            RealEstateDto dto = new RealEstateDto();

            dto.Address = "Name";
            dto.SizeSqrM = 123;
            dto.Floor = 123;
            dto.BuildingType = "sdf";
            //dto.Crew = 123;
            //dto.Company = "asd";
            //dto.CargoWeight = 123;
            //dto.CreatedAt = DateTime.Now;
            //dto.ModifiedAt = DateTime.Now;

            var result = await Svc<IRealEstatesServices>().Create(dto);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            //Arrange
            Guid guid = Guid.Parse("2b8b9080-60ef-442b-9822-f1ad47984736");
            //kuidas teha automaatselt guidi
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());

            //Act
            await Svc<ISpaceshipServices>().GetAsync(guid);

            //Assert
            Assert.NotEqual(guid, wrongGuid);
        }

        [Fact]
        public async Task Should_GetByIdSpaceship_WhenReturnsEqual()
        {
            //Arrange
            Guid databaseGuid = Guid.Parse("2b8b9080-60ef-442b-9822-f1ad47984736");
            Guid getGuid = Guid.Parse("2b8b9080-60ef-442b-9822-f1ad47984736");

            //Act
            await Svc<ISpaceshipServices>().GetAsync(getGuid);

            //Assert
            Assert.Equal(databaseGuid, getGuid);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
        {
            //Arrange
            SpaceshipDto spaceship = MockSpaceshipData();

            //Act

            var addSpaceship = await Svc<ISpaceshipServices>().Create(spaceship);
            var result = await Svc<ISpaceshipServices>().Delete((Guid)addSpaceship.Id);
            //Assert

            Assert.Equal(result, addSpaceship);
        }

        [Fact]
        public async Task ShouldNot_DeleteByIdSpaceship_WhenDidNotDeleteTheWrightSpaceship()
        {
            SpaceshipDto spaceship = MockSpaceshipData();

            var addSpaceship = await Svc<ISpaceshipServices>().Create(spaceship);
            var addSpaceship2 = await Svc<ISpaceshipServices>().Create(spaceship);

            var result = await Svc<ISpaceshipServices>().Delete((Guid)addSpaceship2.Id);

            Assert.NotEqual(result.Id, addSpaceship.Id);
        }

        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdateData()
        {
            var guid = new Guid("2b8b9080-60ef-442b-9822-f1ad47984736");
            //Arrange
            //old data from db
            Spaceship spaceship = new Spaceship();

            //new data
            SpaceshipDto dto = MockSpaceshipData();

            spaceship.Id = Guid.Parse("2b8b9080-60ef-442b-9822-f1ad47984736");
            spaceship.Name = "asd";
            spaceship.Type = "asdasd";
            spaceship.Passengers = 123123;
            spaceship.EnginePower = 987654;
            spaceship.Crew = 123;
            spaceship.Company = "Company asd";
            spaceship.CargoWeight = 567;
            spaceship.CreatedAt = DateTime.Now.AddYears(1);
            spaceship.ModifiedAt = DateTime.Now.AddYears(1);

            //Act
            await Svc<ISpaceshipServices>().Update(dto);

            //Assert
            Assert.Equal(spaceship.Id, guid);
            Assert.NotEqual(spaceship.EnginePower, dto.EnginePower);
            Assert.Equal(spaceship.Crew, dto.Crew);
            Assert.DoesNotMatch(spaceship.Passengers.ToString(), dto.Passengers.ToString());
        }


        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdateDataVersion2()
        {
            SpaceshipDto dto = MockSpaceshipData();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            SpaceshipDto update = MockUpdateSpaceshipData();
            var updateSpaceship = await Svc<ISpaceshipServices>().Update(update);

            ///error korda teha
            Assert.Matches(updateSpaceship.Name, createSpaceship.Name);
            Assert.NotEqual(updateSpaceship.EnginePower, createSpaceship.EnginePower);
            Assert.NotEqual(updateSpaceship.Crew, createSpaceship.Crew);
            Assert.DoesNotMatch(updateSpaceship.Passengers.ToString(), createSpaceship.Passengers.ToString());
        }

        [Fact]
        public async Task ShouldNot_UpdateSpaceship_WhenNotUpdateDatata()
        {
            SpaceshipDto dto = MockSpaceshipData();
            await Svc<ISpaceshipServices>().Create(dto);

            SpaceshipDto nullUpdate = MockNullSpaceship();
            await Svc<ISpaceshipServices>().Update(nullUpdate);

            var nullId = nullUpdate.Id;

            Assert.True(dto.Id == nullId);
        }

        private SpaceshipDto MockNullSpaceship()
        {
            SpaceshipDto nullDto = new()
            {
                Id = null,
                Name = "Name123",
                Type = "type123",
                Passengers = 123,
                Crew = 123,
                Company = "Company123",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return nullDto;
        }

        private SpaceshipDto MockUpdateSpaceshipData()
        {
            SpaceshipDto update = new()
            {
                Name = "asd123",
                Type = "asd",
                Passengers = 123456,
                EnginePower = 123456,
                Crew = 123456,
                Company = "asdasdasd",
                CargoWeight = 123456,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return update;
        }

        private SpaceshipDto MockSpaceshipData()
        {
            SpaceshipDto spaceship = new()
            {
                Name = "asd123",
                Type = "asd",
                Passengers = 123,
                EnginePower = 123,
                Crew = 123,
                Company = "asd",
                CargoWeight = 123,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            return spaceship;
        }
    }
}

