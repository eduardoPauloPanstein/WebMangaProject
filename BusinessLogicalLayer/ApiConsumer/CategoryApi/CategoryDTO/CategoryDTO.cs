// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AnimeCate
{
    public LinksCate links { get; set; }
}

public class AttributesCate
{
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int totalMediaCount { get; set; }
    public string slug { get; set; }
    public bool nsfw { get; set; }
    public int childCount { get; set; }
}

public class DataCate
{
    public string id { get; set; }
    public string type { get; set; }
    public LinksCate links { get; set; }
    public AttributesCate attributes { get; set; }
    public RelationshipsCate relationships { get; set; }
}

public class DramaCate
{
    public LinksCate links { get; set; }
}

public class LinksCate
{
    public string self { get; set; }
    public string related { get; set; }
}

public class MangaCate
{
    public LinksCate links { get; set; }
}

public class ParentCate
{
    public LinksCate links { get; set; }
}

public class RelationshipsCate
{
    public ParentCate parent { get; set; }
    public AnimeCate anime { get; set; }
    public DramaCate drama { get; set; }
    public MangaCate manga { get; set; }
}

public class RootCate
{
    public DataCate data { get; set; }
}

