using DotNetCore.Objects;
using DotNetCoreArchitecture.CrossCutting.Resources;
using DotNetCoreArchitecture.Database;
using DotNetCoreArchitecture.Domain;
using DotNetCoreArchitecture.Infra;
using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public sealed class {{Name}}ApplicationService : I{{Name}}ApplicationService
    {
        public {{Name}}ApplicationService
        (
            IUnitOfWork unitOfWork,
            I{{Name}}Repository _{{Name}}Repository
        )
        {
            UnitOfWork = unitOfWork;
            {{Name}}Repository = _{{Name}}Repository;
        }
        
        private IUnitOfWork UnitOfWork { get; }
        
        private I{{Name}}Repository {{Name}}Repository { get; }

        public async Task<IDataResult<long>> AddAsync(Add{{Name}}Model add{{Name}}Model)
        {
            var validation = new Add{{Name}}ModelValidator().Valid(add{{Name}}Model);
			
			if (!validation.IsSuccess)
            {
                return DataResult<long>.Error(validation.Message);
            }
            
            var {{Name}}Entity = {{Name}}EntityFactory.Create(add{{Name}}Model);

            {{Name}}Entity.Add();

            await {{Name}}Repository.AddAsync({{Name}}Entity);

            await UnitOfWork.SaveChangesAsync();
			
            return DataResult<long>.Success({{Name}}Entity.Id);
        }

        public async Task<IResult> DeleteAsync(long {{Name}}Id)
        {
            await {{Name}}Repository.DeleteAsync({{Name}}Id);

            await UnitOfWork.SaveChangesAsync();

            return DataResult<long>.Success();
        }

		
        public async Task<PagedList<{{Name}}Model>> ListAsync(PagedListParameters parameters)
        {
            return await {{Name}}Repository.ListAsync<{{Name}}Model>(parameters);
        }

        public async Task<IEnumerable<{{Name}}Model>> ListAsync()
        {
            return await {{Name}}Repository.ListAsync<{{Name}}Model>();
        }

        public async Task<{{Name}}Model> SelectAsync(long {{Name}}Id)
        {
            return await {{Name}}Repository.SelectAsync<{{Name}}Model>({{Name}}Id);
        }


        public async Task<IResult> UpdateAsync(Update{{Name}}Model update{{Name}}Model)
        {
            var validation = new Update{{Name}}ModelValidator().Valid(update{{Name}}Model);
			
			if (!validation.IsSuccess)
            {
                return DataResult<long>.Error(validation.Message);
            }

            var {{Name}}Entity = await {{Name}}Repository.SelectAsync(update{{Name}}Model.Id);

            if ({{Name}}Entity == default)
            {
                return DataResult<string>.Success();
            }
            
            await {{Name}}Repository.UpdateAsync({{Name}}Entity.Id, {{Name}}Entity);

            await UnitOfWork.SaveChangesAsync();

             return DataResult<string>.Success();
        }
    }
}
