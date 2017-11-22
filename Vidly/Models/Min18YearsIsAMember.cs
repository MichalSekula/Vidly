using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    //Jest to klasa dzieki ktorej stworzymy nasz customowy Walidator
    public class Min18YearsIsAMember:ValidationAttribute
    {
        //Klasa nadpisuje metode wbudowana ktora zwraca nam wynik walidacji.
        //Jesli warunek zostaje spelniony to walidacja zakonczyla sie sukcesem (sucess)
        //Jesli nie mozemy wpisac w nawiasy, co ma byc wyswietlone przy walidacji danego pola 
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == MembershipTypes.Unknow || customer.MembershipTypeId == MembershipTypes.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if(customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be at least 18 years old to go on a membership.");

        }
    }
}