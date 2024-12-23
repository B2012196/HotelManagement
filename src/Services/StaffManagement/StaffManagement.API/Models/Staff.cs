﻿namespace StaffManagement.API.Models
{
    public class Staff
    {
        public Guid StaffId { get; set; }
        public Guid UserId { get; set; }
        public Guid HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateofBirst { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public DateOnly HireDate { get; set; }
    }
}
