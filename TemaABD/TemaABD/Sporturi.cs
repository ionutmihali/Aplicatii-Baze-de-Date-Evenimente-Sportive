//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TemaABD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sporturi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sporturi()
        {
            this.SporturiStudents = new HashSet<SporturiStudent>();
            this.Antrenoris = new HashSet<Antrenori>();
            this.Echipes = new HashSet<Echipe>();
        }
    
        public int IDSport { get; set; }
        public string denumire { get; set; }
        public string tip { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SporturiStudent> SporturiStudents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antrenori> Antrenoris { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Echipe> Echipes { get; set; }
    }
}
