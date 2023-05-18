public class AnimeCharacters
{
    public LinksANI? links { get; set; }
}

public class AnimeProductions
{
    public LinksANI? links { get; set; }
}

public class AnimeStaff
{
    public LinksANI? links { get; set; }
}

public class AttributesANI
{
    public DateTime? createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
    public string? slug { get; set; }
    public string? synopsis { get; set; }
    public string? description { get; set; }
    public int? coverImageTopOffset { get; set; }
    public TitlesANI? titles { get; set; }
    public string? canonicalTitle { get; set; }
    public List<string?> abbreviatedTitles { get; set; }
    public string? averageRating { get; set; }
    public ratingFrequencies? ratingFrequencies { get; set; }
    public int? userCount { get; set; }
    public int? favoritesCount { get; set; }
    public string? startDate { get; set; }
    public string? endDate { get; set; }
    public object? nextRelease { get; set; }
    public int? popularityRank { get; set; }
    public int? ratingRank { get; set; }
    public string? ageRating { get; set; }
    public string? ageRatingGuide { get; set; }
    public string? subtype { get; set; }
    public string? status { get; set; }
    public string? tba { get; set; }
    public PosterImage? posterImage { get; set; }
    public CoverImage? coverImage { get; set; }
    public int? episodeCount { get; set; }
    public int? episodeLength { get; set; }
    public int? totalLength { get; set; }
    public string? youtubeVideoId { get; set; }
    public string? showType { get; set; }
    public bool? nsfw { get; set; }
}

public class CastingsANI
{
    public LinksANI? links { get; set; }
}

public class CategoriesANI
{
    public LinksANI? links { get; set; }
}

public class CharactersANI
{
    public LinksANI? links { get; set; }
}

public class CoverImageANI
{
    public string? tiny { get; set; }
    public string? large { get; set; }
    public string? small { get; set; }
    public string? original { get; set; }
    public Meta? meta { get; set; }
}

public class DataANI
{
    public string? id { get; set; }
    public string? type { get; set; }
    public LinksANI? links { get; set; }
    public AttributesANI? attributes { get; set; }
    public RelationshipsANI? relationships { get; set; }
}

public class DimensionsANI
{
    public TinyANI? tiny { get; set; }
    public LargeANI? large { get; set; }
    public SmallANI? small { get; set; }
    public MediumANI? medium { get; set; }
}

public class Episodes
{
    public LinksANI? links { get; set; }
}

public class GenresANI
{
    public LinksANI? links { get; set; }
}

public class InstallmentsANI
{
    public LinksANI? links { get; set; }
}

public class LargeANI
{
    public int? width { get; set; }
    public int? height { get; set; }
}

public class LinksANI
{
    public string? self { get; set; }
    public string? related { get; set; }
}

public class MappingsANI
{
    public LinksANI? links { get; set; }
}

public class MediaRelationshipsANI
{
    public LinksANI? links { get; set; }
}

public class MediumANI
{
    public int? width { get; set; }
    public int? height { get; set; }
}

public class MetaANI
{
    public DimensionsANI? dimensions { get; set; }
}

public class PosterImageANI
{
    public string? tiny { get; set; }
    public string? large { get; set; }
    public string? small { get; set; }
    public string? medium { get; set; }
    public string? original { get; set; }
    public MetaANI? meta { get; set; }
}

public class ProductionsANI
{
    public LinksANI? links { get; set; }
}

public class QuotesANI
{
    public LinksANI? links { get; set; }
}

public class ratingFrequencies
{
    public string? _2 { get; set; }
    public string? _3 { get; set; }
    public string? _4 { get; set; }
    public string? _5 { get; set; }
    public string? _6 { get; set; }
    public string? _7 { get; set; }
    public string? _8 { get; set; }
    public string? _9 { get; set; }
    public string? _10 { get; set; }
    public string? _11 { get; set; }
    public string? _12 { get; set; }
    public string? _13 { get; set; }
    public string? _14 { get; set; }
    public string? _15 { get; set; }
    public string? _16 { get; set; }
    public string? _17 { get; set; }
    public string? _18 { get; set; }
    public string? _19 { get; set; }
    public string? _20 { get; set; }
}

public class RelationshipsANI
{
    public GenresANI? genres { get; set; }
    public CategoriesANI? categories { get; set; }
    public CastingsANI? castings { get; set; }
    public InstallmentsANI? installments { get; set; }
    public MappingsANI? mappings { get; set; }
    public ReviewsANI? reviews { get; set; }
    public MediaRelationshipsANI? mediaRelationships { get; set; }
    public CharactersANI? characters { get; set; }
    public StaffANI? staff { get; set; }
    public ProductionsANI? productions { get; set; }
    public QuotesANI? quotes { get; set; }
    public Episodes? episodes { get; set; }
    public StreamingLinksANI? streamingLinks { get; set; }
    public AnimeProductions? animeProductions { get; set; }
    public AnimeCharacters? animeCharacters { get; set; }
    public AnimeStaff? animeStaff { get; set; }
}

public class ReviewsANI
{
    public LinksANI links { get; set; }
}

public class RootANI
{
    public DataANI data { get; set; }
}

public class SmallANI
{
    public int? width { get; set; }
    public int? height { get; set; }
}

public class StaffANI
{
    public LinksANI links { get; set; }
}

public class StreamingLinksANI
{
    public LinksANI links { get; set; }
}

public class TinyANI
{
    public int? width { get; set; }
    public int? height { get; set; }
}

public class TitlesANI
{
    public string? en_jp { get; set; }
    public string? ja_jp { get; set; }
}
