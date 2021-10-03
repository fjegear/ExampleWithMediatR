using ExampleWithMediatR.Data.RepositoriesAbstractions;
using ExampleWithMediatR.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleWithMediatR.ApplicationServices.Subscribers
{
    public class CustomerCreatedHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;

        public CustomerCreatedHandler(ICustomerRepository customerRepository, ILogger<CustomerCreatedHandler> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger;
        }

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(c => c.Id == notification.CustomerId);

            if (customer == null)
            {
                _logger.LogWarning("Customer is not found by customer id from publisher");
            }
            else
            {
                _logger.LogInformation($"Customer has found by customer id: {notification.CustomerId} from publisher");
            }
        }
    }
}
