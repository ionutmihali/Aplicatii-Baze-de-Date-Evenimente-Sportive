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
    
    public partial class Antrenori
    {
        public int IDAntrenori { get; set; }
        public Nullable<int> IDUtilizator { get; set; }
        public Nullable<int> IDSport { get; set; }
    
        public virtual Sporturi Sporturi { get; set; }
        public virtual Utilizatori Utilizatori { get; set; }
    }
}
