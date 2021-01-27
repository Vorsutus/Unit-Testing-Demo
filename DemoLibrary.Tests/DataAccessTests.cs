using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DemoLibrary;
using DemoLibrary.Models;

namespace DemoLibrary.Tests
{
    public class DataAccessTests
    {
        [Fact]
        public void AddPersonToPeopleList_ShouldWork()
        {
            //Arrange
            PersonModel newPerson = new PersonModel { FirstName = "Chris", LastName = "Ross" };
            List<PersonModel> people = new List<PersonModel>();

            //Act -- Adds a newPerson to the People List
            //A list is an instantiated reference type, so it's not a different list
            DataAccess.AddPersonToPeopleList(people, newPerson);

            //Asset
            Assert.True(people.Count == 1);
            Assert.Contains<PersonModel>(newPerson, people);
        }

        [Theory]
        [InlineData("Chris", "", "LastName")]
        [InlineData("", "Ross", "FirstName")]
        public void AddPersonToPeopleList_ShouldFail(string firstName, string lastName, string param)
        {
            //Arrange
            PersonModel newPerson = new PersonModel { FirstName = firstName, LastName = lastName };
            List<PersonModel> people = new List<PersonModel>();


            //Assert and Act -- the parameter that is expected to fail is called "param".
            //The code we test is the Method with the List and the newPerson.
            //We expect it to fail with the specific Argument Exception for whatever we pass in for "param".
            Assert.Throws<ArgumentException>(param, () => DataAccess.AddPersonToPeopleList(people, newPerson));
        }

        //TODO - Add Unit Test for ConvertModelsToCSV()

        //TODO - Add Unit Test for GetAllPeople()
    }
}
