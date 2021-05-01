using ProductsCatalogManagement.Core.Entities.Base;
using System;

namespace ProductsCatalogManagement.Core.Entities
{
    public class Product : Entity
    {
        /// <summary>
        /// The product code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The product start validity date.
        /// </summary>
        public DateTime StartValidityDate { get; set; }

        /// <summary>
        /// The product end validity date.
        /// </summary>
        public DateTime EndValidityDate { get; set; }
    }
}