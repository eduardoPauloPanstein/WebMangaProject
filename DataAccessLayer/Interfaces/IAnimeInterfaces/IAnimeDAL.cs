using Entities.AnimeS;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.IAnimeInterfaces
{
    public interface IAnimeDAL : ICRUD<Anime>,IAnimeGet,IAnimePost
    {
    }
}
