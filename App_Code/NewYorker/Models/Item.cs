using System.Collections.Generic;
using System.Runtime.Serialization;

public partial class Models
{

    /// <summary>
    /// Summary description for Item
    /// </summary>
    [DataContract]
    public class Item
    {
        [DataMember] public string global_item_id { get; set; }
        [DataMember] public string id { get; set; }
        [DataMember] public string country { get; set; }
        [DataMember] public string maintenance_group { get; set; }
        [DataMember] public string web_category_id { get; set; }
        [DataMember] public string web_category { get; set; }
        [DataMember] public string brand { get; set; }
        [DataMember] public int sales_unit { get; set; }
        [DataMember] public string customer_group { get; set; }
        [DataMember] public List<Variant> variants { get; set; }
        [DataMember] public List<Description> descriptions { get; set; }
    }
}