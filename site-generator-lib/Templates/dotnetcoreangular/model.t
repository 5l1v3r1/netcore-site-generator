using System;

namespace WebApplication.Models
{
    public class {{Name}}
    {
		{{#Fields}}
			public {{Type}} {{Name}} { get; set; }
		{{/Fields}}
    }
}