using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Recipies
{
    public class Recipie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public int? HealthGroupId { get; set; }
        public HealthGroup HealthGroup { get; set; }
    }
}
