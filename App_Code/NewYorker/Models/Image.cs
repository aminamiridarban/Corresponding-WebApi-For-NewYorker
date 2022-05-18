using System.Runtime.Serialization;

public partial class Models
{

    /// <summary>
    /// Summary description for Image
    /// </summary>
    [DataContract]
    public class Image
    {
        [DataMember] public string key { get; set; }
        [DataMember] public string type { get; set; }
        [DataMember] public string angle { get; set; }
        [DataMember] public bool has_thumbnail { get; set; }
        [DataMember] public int position { get; set; }
    }
}