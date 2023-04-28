using System.Diagnostics;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Attributes;
using SharedLibrary.Repositories.Actions;
using SharedLibrary.Repositories.Interfaces;
using SharedLibrary.Models;

namespace SharedLibrary.Controllers
{
    [Route("apiv2/[controller]")]
    [ApiController]
    public abstract class GenericController<Repository,
        Entity,
        CreateEntity,
        UpdateEntity> : ControllerBase
        where Entity : class, IEntity
        where Repository : IGenericRepository<Entity>
        where CreateEntity : class
        where UpdateEntity : IPrimary
    {
        protected Repository _repository;
        protected IMapper _mapper;

        public GenericController(Repository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, int take = 8)
        {
            (IQueryable<Entity> Entity, int TotalPage) = _repository.Paginated(page, take);
            return Ok(new { Entity, TotalPage });
        }

        [HttpGet("{Id}")]
        public IActionResult Index(long Id)
        {
            return Ok(_repository.GetById(Id));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Add(CreateEntity createUser)
        {

            return Ok(await Creating(createUser));
        }
        virtual protected Task<Entity> Creating(CreateEntity create)
        {
            return Task.FromResult(_repository.Add<CreateEntity>(_mapper, create));
        }
        virtual protected Task<Entity> Updating(UpdateEntity update)
        {
            return Task.FromResult(_repository.Update<UpdateEntity>(_mapper, update));
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateEntity updateUser)
        {
            try
            {
                return Ok(await Updating(updateUser));
            }
            catch (Exception exception)
            {
                return NotFound();
            }

        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Entity entity)
        {
            _repository.Remove(entity);

            return Ok();
        }

    }
}
