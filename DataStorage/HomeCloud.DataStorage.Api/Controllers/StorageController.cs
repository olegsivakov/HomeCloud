namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	#endregion

	public class Model
	{
		public int ID { get; set; }
	}

	public class StorageController : Controller
	{
		[HttpGet("v1/[controller]s")]
		[MapToApiVersion("0.5")]
		public async Task<IActionResult> Get(int offset = 0, int limit = 20)
		{
			return this.Ok(System.Linq.Enumerable.Empty<Model>());
		}

		[HttpGet("v1/[controller]s/{id:int}")]
		public async Task<IActionResult> Get(int id)
		{
			Model model = null;
			if (model == null)
			{
				return this.NotFound();
			}

			return this.Ok(id);
		}

		[HttpPost("v1/[controller]s")]
		public async Task<IActionResult> Post(Model model)
		{
			if (model == null)
			{
				return this.BadRequest();
			}

			return this.Ok(model);
		}

		[HttpPut("v1/[controller]s/{id:int}")]
		public async Task<IActionResult> Put(int id, [FromBody] Model model)
		{
			if (model == null || model.ID != id)
			{
				return BadRequest();
			}

			if (model == null)
			{
				return this.NotFound();
			}

			return this.NoContent();
		}

		[HttpDelete("v1/[controller]s/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			Model model = null;
			if (model == null)
			{
				return this.NotFound();
			}

			return this.NoContent();
		}
	}
}
