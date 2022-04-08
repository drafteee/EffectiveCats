using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MBase
{
    public interface IDeleteCommand : IRequest<long>
    {
        long Id { get; set; }
    }

    public class DeleteCommandHandler<TEntity, TCommand, TService> : IRequestHandler<TCommand, long>
        where TEntity : class
        where TCommand : IDeleteCommand
        where TService : ICRUD<TEntity>
    {
        private readonly TService _service;
        private readonly ILogger _logger;

        public DeleteCommandHandler(ILogger logger, TService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<long> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"DeleteM {typeof(TEntity).Name} [{DateTime.Now}]");

                return await _service.Delete(command.Id);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"DeleteM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}
