﻿using ExampleWithMediatR.Domain.Dtos;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ExampleWithMediatR.Domain.Commands
{
    public class CreateCustomerCommand : CommandBase<CustomerDto>
    {
        [JsonProperty("name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [JsonProperty("email")]
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        [JsonProperty("address")]
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [JsonProperty("age")]
        [Required]
        public int Age { get; set; }
        [JsonProperty("phone_number")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public CreateCustomerCommand()
        {
        }

        [JsonConstructor]
        public CreateCustomerCommand(string name, string email, string address, int age, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            Age = age;
            PhoneNumber = phoneNumber;
        }
    }
}
