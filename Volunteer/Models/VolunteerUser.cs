﻿using Microsoft.AspNetCore.Identity;

namespace Volunteer.Models
{
    public class VolunteerUser:ApplicationUser
    {
        
        public DateTime DateOfBirth { get; set; }
        public string City { get; set; }    
        public string About { get; set; }   
    }

}
