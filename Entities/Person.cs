using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities
{
    [DataContract]
    [Index(nameof(Email), IsUnique = true)]
    public class Person
    {
        [DataMember]
        public int Id { get; set; }

        [NotMapped]
        public string Name => $"{FirstName} {LastName}";

        [DataMember]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name exceeds {1} characters.")]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(150, ErrorMessage = "Last name exceeds {1} characters.")]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(320, ErrorMessage = "Email exceeds {1} characters.")]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "Age")]
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        [DataMember]
        [Display(Name = "Last Modified")]
        [ConcurrencyCheck]
        public DateTime LastModified { get; set; }

        [DataMember]
        [Display(Name = "Created")]
        public DateTime Created { get; set; }
    }

}
