using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Entities
{
    // the table DataAnotatin gives the naem table to 
    [Table("Authors")]
    public class Author
    {
      [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]

        public String FirstName { get; set; }
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }
    }
}
