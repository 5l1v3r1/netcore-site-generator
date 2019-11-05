using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System;

namespace DotNetCoreArchitecture.Domain
{
    public class {{Name}}Entity
    {
        public {{Name}}Entity
        (
		{{#prop_list Fields}}{{/prop_list}}
        )
        {
        {{#Fields}}
			{{Name}} = _{{Name}};
		{{/Fields}}
        }

        public {{Name}}Entity(string _Id)
        {
            Id = _Id;
        }

		{{#Fields}}
			public {{Type}} {{Name}} { get; private set; }
		{{/Fields}}
		
        public void Add()
        {
        }

    }
}
