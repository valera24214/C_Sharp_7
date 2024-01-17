namespace WindowsFormsApp1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            Orders = new HashSet<Orders>();
        }

        public int id { get; set; }

        public int id_Author { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        public int id_Publishing { get; set; }

        [StringLength(50)]
        public string Binding { get; set; }

        public virtual Authors Authors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        public virtual Publishings Publishings { get; set; }
    }
}
