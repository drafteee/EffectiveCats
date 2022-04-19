using AutoMapper;
using BL.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MBase
{
    public interface ICreateCommand<TId> : IRequest<TId> { }

    public class CreateCommandHandler<TEntity, TId, TCommand, TService> : IRequestHandler<TCommand, TId>
        where TEntity : class
        where TCommand : ICreateCommand<TId>
        where TService : ICRUD<TEntity, TId>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly TService _service;

        public CreateCommandHandler(ILogger logger, IMapper mapper, TService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public virtual Task<TId> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"CreateM {typeof(TEntity).Name} [{DateTime.Now}]");

                var entity = _mapper.Map<TCommand, TEntity>(command);

                return _service.Create(entity);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"CreateM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}
