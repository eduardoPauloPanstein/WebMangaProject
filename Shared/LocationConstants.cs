namespace Shared
{
    public class LocationConstants
    {
        public static readonly string IdNotNullMessage = "ID deve ser informado.";
        public static NicknameConstants Nickname = new(); 
        public static PasswordConstants Password = new(); 
        public static CacheKey CacheKey  = new(); 

        public const int EmailMaxLength = 256; // RFC 5321

    }
    public class CacheKey
    {
        public AnimeCacheKey Anime = new();
        public MangaCacheKey Manga = new();

    }
    public class AnimeCacheKey
    {
        public readonly string GetTop7AnimesCatalogByUserCount;
        public readonly string GetTop7AnimesCatalogByFavorites;
        public readonly string GetTop7AnimesCatalogByRating;
        public readonly string GetByPopularity;


        public AnimeCacheKey()
        {
            GetTop7AnimesCatalogByUserCount = "GetTop7AnimesCatalogByUserCount";
            GetTop7AnimesCatalogByFavorites = "GetTop7AnimesCatalogByFavorites";
            GetTop7AnimesCatalogByRating = "GetTop7AnimesCatalogByRating";
            GetByPopularity = "GetAnimeByPopularity";
        }

    }
    public class MangaCacheKey
    {
        public readonly string GetTop7MangasCatalogByUserCount;
        public readonly string GetTop7MangasCatalogByFavorites;
        public readonly string GetTop7MangasCatalogByRating;
        public readonly string GetByPopularity;

        public MangaCacheKey()
        {
            GetTop7MangasCatalogByUserCount = "GetTop7MangasCatalogByUserCount";
            GetTop7MangasCatalogByFavorites = "GetTop7MangasCatalogByFavorites";
            GetTop7MangasCatalogByRating = "GetTop7MangasCatalogByRating";
            GetByPopularity = "GetMangaByPopularity";
        }

    }


    public class NicknameConstants
    {
        public readonly int MaxLength;
        public readonly int MinLength;

        public readonly string MaxLengthMessage;
        public readonly string MinLengthMessage;
        public readonly string NotNullMessage;

        public NicknameConstants()
        {
            MaxLength = 20;
            MinLength = 3;
            MaxLengthMessage = $"Nickname não pode conter mais de {MaxLength} caracteres.";
            MinLengthMessage = $"Nickname deve conter pelo menos {MinLength} caracteres.";
            NotNullMessage = "Nickname deve ser informado.";
        }
    }
    public class PasswordConstants
    {
        public readonly int MaxLength;
        public readonly int MinLength;

        public readonly string MaxLengthMessage;
        public readonly string MinLengthMessage;
        public readonly string NotNullMessage;
        public readonly string InvalidPasswordMessage;
        public readonly string ConfirmPasswordMessage;



        public PasswordConstants()
        {
            MaxLength = 15;
            MinLength = 6;
            MaxLengthMessage = $"Senha não pode conter mais de {MaxLength} caracteres.";
            MinLengthMessage = $"Senha deve conter pelo menos {MinLength} caracteres.";
            NotNullMessage = "Senha deve ser informada.";
            InvalidPasswordMessage = "Pelo menos um caractere minusculo.\r\n" +
                                     "Pelo menos um caractere maiusculo.\r\n" +
                                     "Pelo menos um dígito.\r\n" +
                                     "Pelo menos um símbolo.";
            ConfirmPasswordMessage = "As senhas devem corresponder.";
        }
    }

}
