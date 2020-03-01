using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("AllowAll")]
	public class VariantsController: Controller
	{
	}
}
