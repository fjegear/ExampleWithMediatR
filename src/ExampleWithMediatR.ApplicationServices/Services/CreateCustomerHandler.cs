using ExampleWithMediatR.ApplicationServices.Mappers;
using ExampleWithMediatR.Data.RepositoriesAbstractions;
using ExampleWithMediatR.Domain.Commands;
using ExampleWithMediatR.Domain.Dtos;
using ExampleWithMediatR.Domain.Events;
using ExampleWithMediatR.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleWithMediatR.ApplicationServices.Services
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerMapping _customerMapping;
        private readonly IMediator _mediator;

        public CreateCustomerHandler(ICustomerRepository customerRepository, ICustomerMapping customerMapping, IMediator mediator)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _customerMapping = customerMapping ?? throw new ArgumentNullException(nameof(customerMapping));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _customerRepository.EmailExistAsync(request.Email))
            {
                throw new ArgumentException($"This email {request.Email} is already used", nameof(request.Email));
            }

            var customer = new Customer(request.Name, request.Email, request.Address, request.Age, request.PhoneNumber);
            _customerRepository.Add(customer);

            if (await _customerRepository.SaveChangesAsync() == 0)
            {
                throw new ApplicationException();
            }

            await _mediator.Publish(new CustomerCreatedEvent(customer.Id), cancellationToken);

            var customerDto = _customerMapping.MapCustomerDto(customer);
            return customerDto;
        }
    }
}
