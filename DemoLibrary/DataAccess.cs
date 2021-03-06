﻿using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DemoLibrary
{
    public static class DataAccess
    {
        private static string personTextFile = "PersonText.txt";

        public static void AddNewPerson(PersonModel person)
        {
            List<PersonModel> people = GetAllPeople();

            //pass the list and the new person
            AddPersonToPeopleList(people, person);

            //capture as a list of strings so we can debug something about it if needed
            List<string> lines = ConvertModelsToCSV(people);

            //this is a Microsoft method so as long as everything leading up to it is good, it should be good
            File.WriteAllLines(personTextFile, lines);
        }

        //passing in out list of people and our new person
        public static void AddPersonToPeopleList(List<PersonModel> people, PersonModel person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "FirstName");
            }

            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "LastName");
            }

            people.Add(person);
        }

        //passing in the list of persons, returning the list of strings
        public static List<string> ConvertModelsToCSV(List<PersonModel> people)
        {
            List<string> output = new List<string>();

            foreach (PersonModel user in people)
            {
                if (!user.FirstName.All(Char.IsLetter) || string.IsNullOrWhiteSpace(user.FirstName))
                {
                    throw new ArgumentException("You passed in an invalid Character", "FirstName");
                }

                if (!user.LastName.All(Char.IsLetter) || string.IsNullOrWhiteSpace(user.LastName))
                {
                    throw new ArgumentException("You passed in an invalid Name", "LastName");
                }

                output.Add($"{ user.FirstName },{ user.LastName }");
            }

            return output;
        }

        public static List<PersonModel> GetAllPeople()
        {
            List<PersonModel> output = new List<PersonModel>();
            string[] content = File.ReadAllLines(personTextFile);

            return SplitStringsAndAddToPersonList(content, output);
        }

        public static List<PersonModel> SplitStringsAndAddToPersonList(string[] stringContent, List<PersonModel> peopleList)
        {
            foreach (string line in stringContent)
            {
                string[] data = line.Split(',');
                peopleList.Add(new PersonModel { FirstName = data[0], LastName = data[1] });
            }

            return peopleList;
        }
    }
}
