
/// <summary>
/// Summary description for ResponsedModel
/// </summary>

[System.Runtime.Serialization.DataContract]
public partial class Models
{
    public class ResponsedModel
    {
        [System.Runtime.Serialization.DataMember] public int totalCount { get; set; }
        [System.Runtime.Serialization.DataMember] public System.Collections.Generic.List<Item> items { get; set; }
        [System.Runtime.Serialization.DataMember] public string correspondingResponse { get; set; }
    }
    public class CorrespondingResponsedModel
    {
        [System.Runtime.Serialization.DataMember] public int totalCount { get; set; }
        [System.Runtime.Serialization.DataMember] public string filteredProductsHtmlString { get; set; }
        [System.Runtime.Serialization.DataMember] public string pager { get; set; }
        [System.Runtime.Serialization.DataMember] public string filterString { get; set; }
        [System.Runtime.Serialization.DataMember] public Item specialRandomOfferItem { get; set; }
    }
}
