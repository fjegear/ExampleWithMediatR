using ExampleWithMediatR.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleWithMediatR.Domain.Queries
{
    public class GetCustomerQuery : QueryBase<CustomerDto>
    {
        [JsonProperty("id")]
        [Required]
        public Guid CustomerId { get; set; }

        public GetCustomerQuery()
        {
        }

        [JsonConstructor]
        public GetCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
