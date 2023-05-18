using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.ApiConsumer.AnimeApi
{
    public interface IAnimeApiConnect
    {
        Task ConsumeAnime();
    }
}
