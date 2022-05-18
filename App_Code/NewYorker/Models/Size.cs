using System.Runtime.Serialization;

public partial class Models
{

    /// <summary>
    /// Summary description for Size
    /// </summary>
    [DataContract]
    public class Size
    {
        [DataMember] public string size_value { get; set; }
        [DataMember] public string size_name { get; set; }
        [DataMember] public string bar_code { get; set; }
    }
}