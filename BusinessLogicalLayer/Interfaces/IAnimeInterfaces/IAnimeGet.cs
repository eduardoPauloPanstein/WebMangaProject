﻿using Entities.AnimeS;
using Shared.Interfaces;
using Shared.Models.Anime;
using Shared.Responses;

namespace BusinessLogicalLayer.Interfaces.IAnimeInterfaces
{
    public interface IAnimeGet : IUsualGetInterfaces<AnimeCatalog,Anime>
    {
       
    }
}
