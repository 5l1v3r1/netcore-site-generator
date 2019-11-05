using System.Collections.Generic;

namespace site_generator_lib.Models
{
    public class Website
    {
        public string Name;
        public List<WebsiteEntity> Entities = new List<WebsiteEntity>();
    }
}
