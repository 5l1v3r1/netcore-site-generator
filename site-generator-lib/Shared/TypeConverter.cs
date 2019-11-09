using System;
using System.Collections.Generic;
using System.Text;

namespace site_generator_lib.Shared
{
    public static class TypeConverter
    {
        public static string PostgresToCsharp(string type)
        {
            switch(type)
            {
                case "character varying":   return "string";
                case "integer":   return "int";
                case "boolean":   return "bool";
                case "uuid":   return "Guid";
                case "date":   return "DateTime";
                default: return type;
            }
        }
    }
}
