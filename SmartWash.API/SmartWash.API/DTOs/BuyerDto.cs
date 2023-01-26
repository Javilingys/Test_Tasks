using SmartWash.API.Domain.Entities;
using System.Collections.Generic;

namespace SmartWash.API.DTOs
{
    public class BuyerDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// коллекция всех идентификаторов покупок, когда-либо осуществляемых данным покупателем
        /// </summary>
        public List<int> SalesIds { get; set; }
    }
}
