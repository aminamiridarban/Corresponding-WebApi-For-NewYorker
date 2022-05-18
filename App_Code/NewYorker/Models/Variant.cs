using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public partial class Models
{

    /// <summary>
    /// Summary description for Variant
    /// </summary>
    [DataContract]
    public class Variant
    {
        [DataMember] public string id { get; set; }
        [DataMember] public string product_id { get; set; }
        [DataMember] public DateTime publish_date { get; set; }
        [DataMember] public bool new_in { get; set; }
        [DataMember] public bool coming_soon { get; set; }
        [DataMember] public bool sale { get; set; }
        [DataMember] public string color_name { get; set; }
        [DataMember] public string pantone_color { get; set; }
        [DataMember] public string pantone_color_name { get; set; }
        [DataMember] public int red { get; set; }
        [DataMember] public int green { get; set; }
        [DataMember] public int blue { get; set; }
        [DataMember] public string color_group { get; set; }
        [DataMember] public string basic_color { get; set; }
        [DataMember] public string currency { get; set; }
        [DataMember] public double original_price { get; set; }
        [DataMember] public double current_price { get; set; }
        [DataMember] public bool red_price_change { get; set; }
        [DataMember] public List<Size> sizes { get; set; }
        [DataMember] public List<Image> images { get; set; }
    }
}