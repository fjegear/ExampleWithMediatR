using ExampleWithMediatR.ApplicationServices.Mappers;
using ExampleWithMediatR.Data.RepositoriesAbstractions;
using ExampleWithMediatR.Domain.Dtos;
using ExampleWithMediatR.Domain.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleWithMediatR.ApplicationServices.Services
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerMapping _customerMapping;
        private readonly ILogger _logger;

        public GetCustomerHandler(ICustomerRepository customerRepository, ICustomerMapping customerMapping, ILogger<GetCustomerHandler> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerMapping = customerMapping ?? throw new ArgumentNullException(nameof(customerMapping));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(c => c.Id == request.CustomerId);

            if (customer != null)
            {
                _logger.LogInformation($"Received request get customer Id: {customer.Id}");
                var customerDto = _customerMapping.MapCustomerDto(customer);
                return customerDto;
            }

            return null;
        }
    }
}
