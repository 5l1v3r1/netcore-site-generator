using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Domain
{
    public static class {{Name}}EntityFactory
    {
        public static {{Name}}Entity Create(string Id)
        {
            return new {{Name}}Entity(Id);
        }

        public static {{Name}}Entity Create(Add{{Name}}Model addModel)
        {
            return new {{Name}}Entity
            ( 
				{{#addmodel_list Fields}}{{/addmodel_list}}
            );
        }
    }
}
