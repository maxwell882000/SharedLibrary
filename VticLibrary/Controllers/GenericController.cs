using System.Diagnostics;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using VitcLibrary.Repositories.Actions;
using VitcLibrary.Repositories.Interfaces;
using VticLibrary.Models;

namespace VitcLibrary.Controllers
{
    [Route("apiv2/[controller]")]
    [ApiController]
    public class GenericController<Repository, Entity, DTO> : ControllerBase
        where Repository : IGenericRepository<Entity>
        where Entity : class
        where DTO : IModel
    {
        protected Repository _repository;
        protected IMapper _mapper;
        protected UpdateOrCreateAction<Entity, DTO> updateOrCreate;

        public GenericController(Repository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
            this.updateOrCreate = new UpdateOrCreateAction<Entity, DTO>(mapper, repository);
        }

        // GET: api/<QuestionTemplateController>
        [HttpGet("all")]
        public virtual IActionResult GetAll()
        {
            return Ok(this._repository.GetAll());
        }

        // GET: api/<QuestionTemplateController>
        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(_mapper.ProjectTo<DTO>(this._repository.GetAll()).ToList());
        }

        // GET api/<QuestionTemplateController>/5
        [HttpGet("{id}")]
        public virtual IActionResult Get(long id)
        {
            var entity = this._repository.GetById(id);

            return Ok(_mapper.Map<DTO>(entity));
        }

        // POST api/<QuestionTemplateController>
        [HttpPost]
        public virtual IActionResult Post(DTO value)
        {
            var _value = _mapper.Map<Entity>(value);
            this._repository.saveCommit(() => this._repository.Add(_value));
            //this._repository.commit();
            return Ok(_mapper.Map<DTO>(_value));
        }

        [HttpPut]
        public virtual IActionResult Put([FromBody] DTO value)
        {
            Debug.WriteLine(value.ToString());
            Entity entity = this._repository.GetById(value.Id);
            _mapper.Map<DTO, Entity>(value, entity);
            this._repository.saveCommit(() => this._repository.update(entity));
            //this._repository.commit();
            return Ok();
        }

        // DELETE api/<QuestionTemplateController>/5
        [HttpDelete("{id}")]
        public virtual void Delete(long id)
        {
            this._repository.Remove(this._repository.GetById(id));
            this._repository.commit();
        }
    }
}
