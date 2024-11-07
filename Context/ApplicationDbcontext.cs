using BookExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace BookExchange.Context
{
    public class ApplicationDbcontext :DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext>contextOptions) 
         : base(contextOptions)  
        {

        }
        //code - Approach
        public DbSet<Books> Books { get; set; }
    }
}
