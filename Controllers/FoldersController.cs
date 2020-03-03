using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Models.ApiModels;
using MyProject.Services;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class FoldersController : Controller
    {
        private readonly IFolderRepository _folderRepository;

        public FoldersController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        [HttpPost("create-folder")]
        public IActionResult CreateFolder(ApiFolder apiFolder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var folder = (Folder)apiFolder;

            var folderDb = _folderRepository.Create(folder);

            if (!folderDb)
            {
                return BadRequest();
            }

            return Ok(folderDb);
        }

        [HttpGet("get-root-folders")]
        public IActionResult GetFolders()
        {
            var folders = _folderRepository.GetRootFolders();

            var apiFolders = folders.Select(f => (ApiFolder)f);

            if (apiFolders == null)
            {
                return BadRequest();
            }

            return Ok(apiFolders);
        }

        [HttpGet("byId/{Id}")]
        public IActionResult GetBytId(Guid Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            var folder = _folderRepository.GetFolderById(Id);

            if (folder==null)
            {
                return NotFound();
            }

            var apiFolder = (ApiFolder)folder;
            return Ok(apiFolder);
        }
    }
}