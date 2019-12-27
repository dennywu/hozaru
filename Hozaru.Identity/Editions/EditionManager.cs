using Hozaru.Core.Application.Editions;
using Hozaru.Core.Domain.Repositories;
using Hozaru.Core.Identity.Application.Editions;
using Hozaru.Core.Identity.Application.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Editions
{
    public class EditionManager : HozaruEditionManager
    {
        public const string DefaultEditionName = "Standard";

        public EditionManager(
            IRepository<Edition, int> editionRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureRepository)
            : base(
                editionRepository,
                editionFeatureRepository
            )
        {
        }
    }
}
