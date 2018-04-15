namespace DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("market.Orders")]
    public partial class Orders
    {
        public int id { get; set; }

        public int? id_user { get; set; }

        public int? id_product { get; set; }

        public int? id_state { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date { get; set; }

        public int? count { get; set; }

        public double? full_cost { get; set; }

        public virtual Product Product { get; set; }

        public virtual OrderState OrderState { get; set; }

        public virtual Users Users { get; set; }
    }
}
