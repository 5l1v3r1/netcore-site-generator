using DotNetCore.EntityFrameworkCore;
using DotNetCoreArchitecture.Domain;
using DotNetCoreArchitecture.Model;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Database
{
    public sealed class {{Name}}Repository : EntityFrameworkCoreRelationalRepository<{{Name}}Entity>, I{{Name}}Repository
    {
        public {{Name}}Repository(Context context) : base(context)
        {
        }
    }
}
