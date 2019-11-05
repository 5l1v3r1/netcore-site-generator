using System.Collections.Generic;

namespace site_generator_lib.Models
{
    public class WebsiteEntity
    {
        public string Name;
        public string Description;

        public List<WebsiteEntityField> Fields = new List<WebsiteEntityField>();
    }
}
