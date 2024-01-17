namespace WindowsFormsApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        public int id { get; set; }

        public int id_Worker { get; set; }

        public int id_Book { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public decimal Cost { get; set; }

        public virtual Books Books { get; set; }

        public virtual Workers Workers { get; set; }
    }
}
