using Ping.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping
{
    public interface IMessageClient
    {
        Task AddMessage();
        Task GetMessages();
        Task DeleteMessage();
    }
}
