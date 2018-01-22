using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Publisher.Publisher
{
    public interface IPublisher
    {
        int PublishMessages(int count);
    }
}
