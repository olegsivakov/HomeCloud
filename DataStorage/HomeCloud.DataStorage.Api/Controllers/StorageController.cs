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
		[HttpGet]
		[Route("[controller]s")]
		public async Task<IActionResult> Get(int offset = 0, int limit = 20)
		{
			return this.Ok(new { offset = offset, limit = limit });
		}

		[HttpGet("{id:int}")]
		[Route("[controller]s/{id}")]
		public async Task<IActionResult> Get(int id)
		{
			return this.Ok(id);
		}

		[HttpPost]
		[Route("[controller]s")]
		public async Task<IActionResult> Post(Model model)
		{
			return this.Ok(model);
		}

		[HttpPut("{id:int}")]
		[Route("[controller]s/{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] Model model)
		{
			if (id != model.ID)
			{
				return this.StatusCode(409);
			}

			return this.Ok(id);
		}

		[HttpDelete]
		[Route("[controller]s")]
		public async Task<IActionResult> Delete()
		{
			return this.Ok();
		}

		[HttpDelete("{id:int}")]
		[Route("[controller]s/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return this.Ok(id);
		}
	}
}
