using MyProject.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
	public class Folder: BaseModel
	{
		public Guid Id { get; set; }

		public string ParentId { get; set; }

		public string Name { get; set; }

		public ICollection<Asset> Assets { get; set; }

		public static explicit operator Folder(ApiFolder apiFolder)
		{
			Folder folder = new Folder();

			folder.Name = apiFolder.Name;
			//folder.ParentId = apiFolder.ParentId;

			return folder;
		}
	}
}
