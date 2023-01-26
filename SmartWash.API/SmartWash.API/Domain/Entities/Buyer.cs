using System.Collections.Generic;
using System.Linq;

namespace SmartWash.API.Domain.Entities
{
    /// <summary>
    /// покупатель, лицо, осуществляющее покупку товара или услуги в одной из точек продаж
    /// </summary>
    public sealed class Buyer : Entity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// коллекция всех идентификаторов покупок, когда-либо осуществляемых данным покупателем
        /// </summary>
        public List<int> SalesIds
        {
            get => BuyerSales?.Select(x => x.SaleId).ToList();
        }

        public List<BuyerSale> BuyerSales { get; set; }
    }
}
