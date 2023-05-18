using Entities;
using Entities.MangaS;
using Shared;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces.IMangaInterfaces
{
    public interface IMangaService: IMangaGet,IMangaPost,ICRUD<Manga>
    {
        
    }
}
