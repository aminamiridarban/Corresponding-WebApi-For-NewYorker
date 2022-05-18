using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public partial class Models
{

    /// <summary>
    /// Summary description for Description
    /// </summary>
    [DataContract]
    public class Description
    {
        [DataMember] public string language { get; set; }
        [DataMember] public string description { get; set; }
    }
}