﻿namespace Travista.Models.Domain
{
    public class User
    {
        public int ID_Users { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
    }
}
