using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators.Manga
{
    internal class MangaValidator : AbstractValidator<Entities.MangaS.Manga>
    {
        public void ValidateImages()
        {
            RuleFor(c => c.CoverImageLink).NotNull().WithMessage("CoverImageLink deve ser informado.");
            RuleFor(c => c.PosterImageLink).NotNull().WithMessage("PosterImageLink deve ser informado.");
        }
        //Nome, desc...
    }
}
