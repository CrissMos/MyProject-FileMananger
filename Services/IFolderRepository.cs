using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
	public interface IFolderRepository
	{
		IEnumerable<Folder> GetFolders();

		Folder GetFolderByParentId(Guid ParentId);

		bool Create(Folder folder);
	}
}
