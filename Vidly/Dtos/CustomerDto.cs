using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    //Poniewaz w RESTfull web Api, nie wolno poslugiwac sie parametrami bezposrednio z naszego modelu
    // musimy stworzyc klase posrednia, ktora bedzie reprezentowala nasz model, aby nie operowac bezposrednio na bazie 
    // gdyz moze sie to skonczyc roznymi problemami, atakami hakerskimi, 
    //W tej klasie powinno sie uzywac prostych typow oraz stworzonych specjalnie w Dto ( data transfer object) 
    //Powinnismy sie pozybyc DataAnnotation Display
    public class CustomerDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Min18YearsIsAMember]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToCustomer { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}