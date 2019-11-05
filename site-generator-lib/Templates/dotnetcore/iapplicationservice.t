using DotNetCore.Objects;
using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public interface I{{Name}}ApplicationService
    {
        Task<IDataResult<long>> AddAsync(Add{{Name}}Model add{{Name}}Model);

        Task<IResult> DeleteAsync(long {{Name}}Id);
        
        Task<PagedList<{{Name}}Model>> ListAsync(PagedListParameters parameters);

        Task<IEnumerable<{{Name}}Model>> ListAsync();

        Task<{{Name}}Model> SelectAsync(long {{Name}}Id);
        
        Task<IResult> UpdateAsync(Update{{Name}}Model update{{Name}}Model);
    }
}
