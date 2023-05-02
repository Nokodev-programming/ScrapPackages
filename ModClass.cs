using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapPackages
{
    public class ModClass
    {
        public bool Inject { get; set; } = false;
        public string Name { get; set; } = "Unknown.";
        public string Description { get; set; } = "No description provided";
        public string Path { get; set; } = string.Empty;
    }
}
