using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ProductsCategories.Models
{

    public class Products
    {

        [Key]
        public int ProductId {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public string Description {get; set;}

        [Required]
        public decimal Price {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;

        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public List<Associations> myAssociation {get; set;}


    }
}