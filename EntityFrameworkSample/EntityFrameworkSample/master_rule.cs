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
    
    public partial class master_rule
    {
        public master_rule()
        {
            this.rule_field_map = new HashSet<rule_field_map>();
        }
    
        public int master_rule_id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public string expression { get; set; }
        public string sql_filter { get; set; }
        public int result_field_id { get; set; }
        public sbyte active { get; set; }
        public System.DateTime date_added { get; set; }
        public string user_added { get; set; }
        public Nullable<System.DateTime> date_changed { get; set; }
        public string user_changed { get; set; }
    
        public virtual field field { get; set; }
        public virtual ICollection<rule_field_map> rule_field_map { get; set; }
    }
}