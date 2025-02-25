﻿using MediatR;

namespace ExampleWithMediatR.Domain.Queries
{
    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}
