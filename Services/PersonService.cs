using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using b1.Models;
using CsvHelper;

namespace b1.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _people = new List<Person>
        {
            new Person
            {
                FirstName = "Vu Hoang Truong",
                LastName = "Giang",
                Gender = "Male",
                DateOfBirth = new DateTime(1997, 12, 26),
                PhoneNumber = "",
                BirthPlace = "Hai Phong",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Pham Duc",
                LastName = "Hai",
                Gender = "Male",
                DateOfBirth = new DateTime(1997, 12, 26),
                PhoneNumber = "",
                BirthPlace = "",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Duong Di",
                LastName = "An",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 6, 28),
                PhoneNumber = "",
                BirthPlace = "",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Dang Tuan",
                LastName = "Anh",
                Gender = "Male",
                DateOfBirth = new DateTime(1997, 12, 26),
                PhoneNumber = "",
                BirthPlace = "",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Nguyen Hoang",
                LastName = "Anh",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 11, 25),
                PhoneNumber = "",
                BirthPlace = "Hai Phong",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Ngo Huu",
                LastName = "Toan",
                Gender = "Male",
                DateOfBirth = new DateTime(2001, 11, 25),
                PhoneNumber = "",
                BirthPlace = "Hai Phong",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "miss",
                LastName = "A",
                Gender = "Female",
                DateOfBirth = new DateTime(2001, 11, 25),
                PhoneNumber = "",
                BirthPlace = "Hai Phong",
                IsGraduated = false
            },
        };
        public List<Person> GetAll()
        {
            return _people;
        }

        public List<string> GetFullNames()
        {
            return _people.Select(p => p.FullName).ToList();
        }

        public List<Person> GetMale()
        {
            return _people.Where(p => p.Gender.Equals("Male", StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public Person GetOldest()
        {
            var maxAge = _people.Max(p => p.Age);
            return _people.First(p => p.Age == maxAge);
        }

        public List<Person> GetPeopleByBirthYear(int year)
        {
            return _people.Where(p => p.DateOfBirth.Year == year).ToList();
        }

        public List<Person> GetPeopleByBirthYearGreaterThan(int year)
        {
            return _people.Where(p => p.DateOfBirth.Year > year).ToList();
        }

        public List<Person> GetPeopleByBirthYearLessThan(int year)
        {
            return _people.Where(p => p.DateOfBirth.Year < year).ToList();
        }

        public byte[] GetDataStream()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
            {
                csvWriter.WriteRecords(_people);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}