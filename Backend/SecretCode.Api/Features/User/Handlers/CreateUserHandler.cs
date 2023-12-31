﻿using AutoMapper;
using MediatR;
using SecretCode.Api.Data;
using SecretCode.Api.Features.User.Commands;

namespace SecretCode.Api.Features.User.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly SecretCodeDataContext _context;
    private readonly IMapper _mapper;
    public CreateUserHandler(SecretCodeDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _mapper.Map<Models.User>(request);
            await _context.Database.BeginTransactionAsync();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }
        catch
        {
            await _context.Database.RollbackTransactionAsync();
            
        }
        finally
        {
            await _context.DisposeAsync();
        }
    }
}
