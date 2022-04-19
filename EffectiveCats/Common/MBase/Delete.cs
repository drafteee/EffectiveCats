using BL.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MBase
{
    public interface IDeleteCommand<TId> : IRequest<TId>
    {
        TId Id { get; set; }
    }

    public class DeleteCommandHandler<TEntity, TId, TCommand, TService> : IRequestHandler<TCommand, TId>
        where TEntity : class
        where TCommand : IDeleteCommand<TId>
        where TService : ICRUD<TEntity, TId>
    {
        private readonly TService _service;
        private readonly ILogger _logger;

        public DeleteCommandHandler(ILogger logger, TService service)
        {
            _logger = logger;
            _service = service;
        }

        public Task<TId> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"DeleteM {typeof(TEntity).Name} [{DateTime.Now}]");

                return _service.Delete(command.Id);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"DeleteM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}
