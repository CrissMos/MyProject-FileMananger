using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.ApiModels
{
	public class ApiVariant
	{
		public int Id { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public Enums.VariantType Type { get; set; }

		public string Link { get; set; }

		public Guid AssetId { get; set; }

		public static explicit operator ApiVariant(Variant variant)
		{
			ApiVariant apiVersion = new ApiVariant();

			apiVersion.Id = variant.Id;
			apiVersion.Width = variant.Height;
			apiVersion.Height = variant.Height;
			apiVersion.Type = variant.Type;
			apiVersion.Link = variant.Link;
			apiVersion.AssetId = variant.AssetId;

			return apiVersion;
		}
	}
}
