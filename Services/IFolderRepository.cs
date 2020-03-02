using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
	public interface IFolderRepository
	{
		IEnumerable<Folder> GetRootFolders();

		Folder GetFolderById(Guid Id);

		ICollection<Folder> GetByParentId(string ParentId);

		bool Create(Folder folder);
	}
}
