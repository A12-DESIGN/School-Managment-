﻿namespace Crud.Models
{
    public class User
    {
       
        
            public string Id { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; } // Admin, Student, Teacher
        }

    }

