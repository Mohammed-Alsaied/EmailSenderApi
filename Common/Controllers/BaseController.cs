using AutoMapper;
using Common.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Common
{
    public class BaseController<TEntity, TViewModel> : ControllerBase
        where TEntity : BaseEntity
        where TViewModel : BaseViewModel
    {
        protected readonly IBaseUnitOfWork<TEntity> _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IValidator<TViewModel> _validator;
        protected readonly ILogger<BaseController<TEntity, TViewModel>> _logger;

        public BaseController(IBaseUnitOfWork<TEntity> unitOfWork, IMapper mapper, IValidator<TViewModel> validator, ILogger<BaseController<TEntity, TViewModel>> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<TViewModel>> Get()
        {
            List<TEntity> entities = await _unitOfWork.ReadAsync();
            return entities.Select(product => _mapper.Map<TViewModel>(product));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(Guid id)
        {
            TEntity product = await _unitOfWork.ReadByIdAsync(id);
            TViewModel productViewModel = _mapper.Map<TViewModel>(product);
            return Ok(productViewModel);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromForm] TViewModel productViewModel)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(productViewModel);
            if (!validationResult.IsValid)
                return BadRequest(new { errors = validationResult.Errors });

            var product = _mapper.Map<TEntity>(productViewModel);
            product = await _unitOfWork.CreateAsync(product);
            return Ok(_mapper.Map<TViewModel>(product));
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(Guid id)
        {
            await _unitOfWork.DeleteAsync(id);
        }
    }
}