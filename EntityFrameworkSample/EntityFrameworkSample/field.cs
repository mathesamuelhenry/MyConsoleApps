//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkSample
{
    using System;
    using System.Collections.Generic;
    
    public partial class field
    {
        public field()
        {
            this.master_rule = new HashSet<master_rule>();
            this.rule_field_map = new HashSet<rule_field_map>();
        }
    
        public int field_id { get; set; }
        public string field_name { get; set; }
        public string sql_column_name { get; set; }
        public sbyte active { get; set; }
        public System.DateTime date_added { get; set; }
        public string user_added { get; set; }
        public Nullable<System.DateTime> date_changed { get; set; }
        public string user_changed { get; set; }
    
        public virtual ICollection<master_rule> master_rule { get; set; }
        public virtual ICollection<rule_field_map> rule_field_map { get; set; }
    }
}
