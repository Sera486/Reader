using Microsoft.AspNetCore.Identity;

namespace Reader.Models
{
    //used to force identity to use int keys instead of guids
    public class ApplicationRole : IdentityRole<int> { }
}
