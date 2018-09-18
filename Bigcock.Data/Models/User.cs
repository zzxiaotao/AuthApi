using System;
using System.Collections.Generic;

namespace Bigcock.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
    }
}
