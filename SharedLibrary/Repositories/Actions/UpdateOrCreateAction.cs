using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Repositories.Interfaces;

namespace SharedLibrary.Repositories.Actions
{
    public class UpdateOrCreateAction<Model, DTO> where Model : class
    {
        private IGenericRepository<Model> repository;
        private IMapper mapper;
        public UpdateOrCreateAction(IMapper mapper, IGenericRepository<Model> repository)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Model updateOrCreate(DTO dto, Func<DbSet<Model>, IQueryable<Model>> func)
        {
            try
            {
                Model old = func(this.repository.GetAll()).First();
                this.mapper.Map(dto, old);
                System.Diagnostics.Debug.WriteLine(old.ToString());
                this.repository.Update(old);
                return old;
            }
            catch (Exception excep)
            {
                Model old = this.mapper.Map<Model>(dto);
                this.repository.Add(old);
                return old;
            }

        }
    }
}

