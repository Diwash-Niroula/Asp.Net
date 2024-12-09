using System.ComponentModel.DataAnnotations;

namespace BookExchange.Models
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Bookname { get; set; }

        [Required]
        [Phone]
        public int phoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
