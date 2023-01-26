using System.Collections.Generic;

namespace SmartWash.API.Domain.Entities
{
    public sealed class SalesPoint : Entity
    {
        public string Name { get; set; }
        public List<ProvidedProduct> ProvidedProducts { get; set; }
    }
}
