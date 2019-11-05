using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    [ApiController]
    [RouteController]
    public class {{Name}}Controller : BaseController
    {
        public {{Name}}Controller(I{{Name}}ApplicationService {{Name}}ApplicationService)
        {
            {{Name}}ApplicationService = {{Name}}ApplicationService;
        }

        private I{{Name}}ApplicationService {{Name}}ApplicationService { get; }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Add{{Name}}Model add{{Name}}Model)
        {
            return Result(await {{Name}}ApplicationService.AddAsync(add{{Name}}Model));
        }

        [AuthorizeEnum(Roles.Admin)]
        [HttpDelete("{{Name | append: "}" | prepend: "{" }}Id")]
        public async Task<IActionResult> DeleteAsync(long {{Name}}Id)
        {
            return Result(await {{Name}}ApplicationService.DeleteAsync({{Name}}Id));
        }

        [HttpGet("Grid")]
        public async Task<PagedList<{{Name}}Model>> GridAsync([FromQuery]PagedListParameters parameters)
        {
            return await {{Name}}ApplicationService.ListAsync(parameters);
        }
		
        [HttpGet]
        public async Task<IEnumerable<{{Name}}Model>> ListAsync()
        {
            return await {{Name}}ApplicationService.ListAsync();
        }

        [HttpGet("{{Name | append: "}" | prepend: "{" }}Id")]
        public async Task<{{Name}}Model> SelectAsync(long {{Name}}Id)
        {
            return await {{Name}}ApplicationService.SelectAsync({{Name}}Id);
        }
		
        [HttpPut("{{Name | append: "}" | prepend: "{" }}Id")]
        public async Task<IActionResult> UpdateAsync(Update{{Name}}Model update{{Name}}Model)
        {
            return Result(await {{Name}}ApplicationService.UpdateAsync(update{{Name}}Model));
        }
    }
}
