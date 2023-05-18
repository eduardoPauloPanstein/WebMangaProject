using Entities;

namespace BusinessLogicalLayer.ApiConsumer.MangaApi.MangaCategoryApi
{
    public class ConverterCategoryToItem
    {
        public static List<Category> CovertiMangaCate(RootMA Cate)
        {
            List<Category> category = new();
            foreach (var item in Cate.data)
            {
                Category c = new Category();
                c.ID = Convert.ToInt32(item.id);
                category.Add(c);
            }
            return category;
        }
    }
}
