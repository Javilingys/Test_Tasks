using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Dtos
{
    public class DeletePongMessageRequestDto
    {
        public int User { get; set; }

        public Guid? Id { get; set; }
    }
}
