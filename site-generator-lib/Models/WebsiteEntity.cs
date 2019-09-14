using System.Collections.Generic;

namespace site_generator_lib.Models
{
    public class WebsiteEntity
    {
        string Name;
        string Description;

        List<WebsiteEntityField> Fields;
    }
}
