using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models.ApiModels
{
	public class ApiFolder
	{
		public Guid Id { get; set; }

		public string ParentId { get; set; }

		public string Name { get; set; }

		public static explicit operator ApiFolder(Folder folder)
		{
			ApiFolder apiFolder = new ApiFolder();

			apiFolder.Id = folder.Id;
			apiFolder.Name = folder.Name;
			apiFolder.ParentId = folder.ParentId;

			return apiFolder;
		}
	}
}
