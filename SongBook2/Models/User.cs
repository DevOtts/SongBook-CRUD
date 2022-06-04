using Microsoft.AspNetCore.Identity;
using System;
namespace SongBook2.Models
{
    public class User : IdentityUser
    {

        public string Name { get; set; }
        public int MusicianType { get; set; }
        public User()
        {
        }
    }
}
