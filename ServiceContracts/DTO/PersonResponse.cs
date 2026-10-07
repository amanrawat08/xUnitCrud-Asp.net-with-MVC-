using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represent the DTO class that is used as the return type of the most method of person service
    /// </summary>
    public class PersonResponse
    {
        public String? PersonName { get; set; }
        public Guid PersonID { get; set; }
        public String? Email { get; set; }

        public DateTime? BirthOfDate { get; set; }
        public Double? Age { get; set; }
        public String? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public String? CountryName { get; set; }
        public String? Address { get; set; }
        public bool? RecieveNewsLetter { get; set; }

        /// <summary>
        /// Compare current obj data with the parameter object 
        /// </summary>
        /// <param name="obj">The Person response object to compare</param>
        /// <returns>True/Fasle/or indicates wheater all person details are matched</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;

            return this.PersonID == person.PersonID && this.Address == person.Address && person.PersonName == this.PersonName && this.Age == person.Age && this.Email == person.Email && this.BirthOfDate == person.BirthOfDate && this.Gender == person.Gender && this.CountryID == person.CountryID && this.RecieveNewsLetter == person.RecieveNewsLetter;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"PersonData: PersonName {PersonName} , Person ID {PersonID} , Country Name: {CountryName} , Email {Email}, CountryID{CountryID} , RecieceNewsLetterx {RecieveNewsLetter}";
        }
        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest(){
            PersonID=PersonID, 
            Address=Address,
            RecieveNewsLetter=RecieveNewsLetter,
            Email = Email,
            PersonName = PersonName,
            BirthOfDate = BirthOfDate,
            Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
            CountryID = CountryID
        };
        }
    }


    public static class PersonExtensions
    {
        /// <summary>
        /// An Extension Method to convert the object of person class into PersonResponse class 
        /// </summary>
        /// <param name="person">Return converted person response object </param>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                Address = person.Address,
                PersonName = person.PersonName,
                Age = (person.BirthOfDate != null) ? Math.Round((DateTime.Now - person.BirthOfDate.Value).TotalDays / 365.25) : null,
                Email = person.Email,
                BirthOfDate = person.BirthOfDate,
                Gender = person.Gender,
                CountryID = person.CountryID,
                RecieveNewsLetter = person.RecieveNewsLetter,
            };
        }
    }
}