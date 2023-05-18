// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class DatumMA
{
    public string type { get; set; }
    public string id { get; set; }
}

public class LinksMA
{
    public string self { get; set; }
    public string related { get; set; }
}

public class RootMA
{
    public List<DatumMA> data { get; set; }
    public Links linksMA { get; set; }
}
