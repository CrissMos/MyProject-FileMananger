using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
	public interface IVariantRepository
	{
		IEnumerable<Variant> GetVariantsByAssetId(Guid AssetId);

		Variant GetThumbnailByAssetId(Guid AssetId);

		bool Create(Variant variant);
	}
}
