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

        [Theory]
        [InlineData("", "Ross", "FirstName")]
        [InlineData("Chris-", "Ross", "FirstName")]
        [InlineData("Chris M", "Ross", "FirstName")]
        [InlineData("Chris", "", "LastName")]
        [InlineData("Chris", "Ross2", "LastName")]
        [InlineData("Chris", "Ross Jr", "LastName")]
        public void ConvertModelsToCSV_InValidNameShouldFail(string firstName, string lastName, string param)
        {
            //Arrange --create a test case user and add them to the People List
            PersonModel newPerson = new PersonModel { FirstName = firstName, LastName = lastName };
            List<PersonModel> people = new List<PersonModel>();
            people.Add(newPerson);
            
            //Assert the exception as the expected fail from the return value
            Assert.Throws<ArgumentException>(param, () => DataAccess.ConvertModelsToCSV(people));
        }

        [Fact]
        public void CovertModelsToCSV_ValidNameShouldWork()
        {
            //Arrange
            PersonModel newPerson1 = new PersonModel { FirstName = "Chris", LastName = "Ross" };
            PersonModel newPerson2 = new PersonModel { FirstName = "Michelle", LastName = "Ross" };
            List<PersonModel> people = new List<PersonModel>();
            people.Add(newPerson1);
            people.Add(newPerson2);

            //Act --Checks the newPerson in the people List with the ConvertModelsToCSV Method and then adds the first entry in that CSV list as a string to the newOutput variable
            var newOutput1 = DataAccess.ConvertModelsToCSV(people)[0].ToString();
            var newOutput2 = DataAccess.ConvertModelsToCSV(people)[1].ToString();

            //Assert that there is only one item in the ConvertModelsToCSV string List
            //Assert that the string matches what we expect it to be
            Assert.True(DataAccess.ConvertModelsToCSV(people).Count == 2);
            Assert.Equal($"{ newPerson1.FirstName },{ newPerson1.LastName }", newOutput1);
            Assert.Equal($"{ newPerson2.FirstName },{ newPerson2.LastName }", newOutput2);
        }

        [Fact]
        public void SplitStringsAndAddToPersonList_ShouldWork()
        {
            //Arrange
            List<PersonModel> people = new List<PersonModel>();
            string[] content = {"Anna,Billy",
                                "Charles,Tim",
                                "Chris,Ross"};
            //Act
            var newOutput = DataAccess.SplitStringsAndAddToPersonList(content, people);

            //Assert
            Assert.True(newOutput.Count == 3);
            Assert.Equal(content[2], newOutput[2].FirstName + "," + newOutput[2].LastName);
        }
    }
}
