using System;

namespace DotNetCoreArchitecture.Model
{
    public class {{Name}}Model
    {
		{{#Fields}}
			public {{Type}} {{Name}} { get; set; }
		{{/Fields}}
    }
}