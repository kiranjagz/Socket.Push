using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Publisher.Publisher
{
    public class PlayerModel
    {
        public int UserId { get; set; }
        public int TotalWagers { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Game { get; set; }
        public string Brand { get; set; }
    }
}
