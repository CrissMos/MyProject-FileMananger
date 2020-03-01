using MyProject.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
	public class Variant : BaseModel
	{
		public int Id { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public Enums.VariantType Type { get; set; }

		public string Link { get; set; }

		[ForeignKey("AssetId")]
		public Asset Asset { get; set; }

		public Guid AssetId { get; set; }

		public static explicit operator Variant(ApiVariant apiVersion)
		{
			Variant v = new Variant();

			v.AssetId = apiVersion.AssetId;

			return v;
		}
	}
}
