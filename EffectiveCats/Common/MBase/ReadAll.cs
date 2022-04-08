using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MBase
{
    public interface IReadAll<TEntityDto> : IRequest<List<TEntityDto>> where TEntityDto : class
    {

    }

    public class ReadAllQueryHandler<TEntity, TQuery, TService, TEntityDto> : IRequestHandler<TQuery, List<TEntityDto>>
        where TEntity : class
        where TEntityDto : class
        where TQuery : IReadAll<TEntityDto>
        where TService : ICRUD<TEntity>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly TService _service;

        public ReadAllQueryHandler(ILogger logger, IMapper mapper, TService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public virtual async Task<List<TEntityDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"ReadAllM {typeof(TEntity).Name} [{DateTime.Now}]");

                return _mapper.Map<List<TEntity>, List<TEntityDto>>(await _service.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogInformation($"ReadAllM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}
