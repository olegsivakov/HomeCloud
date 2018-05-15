using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCloud.IdentityService.Api.Controllers
{
	public class ResourceController
	{
		// {controller}/api GET
		public async Task<IActionResult> GetApiResources(string name, IEnumerable<string> scopes)
		{
		}

		// {controller}/api/{id} GET
		public async Task<IActionResult> GetApiResources(Guid id)
		{
		}

		// {controller}/api POST
		public async Task<IActionResult> CreateApiResources(object resource)
		{
		}

		// {controller}/api/secrets POST
		public async Task<IActionResult> SaveApiResourceSecret(Guid id, string secret)
		{
		}

		// {controller}/api/secrets POST
		public async Task<IActionResult> SaveApiResourceSecret(Guid id, string secret)
		{
		}

		// {controller}/identity POST
		public async Task<IActionResult> CreateIdentityResources(object resource)
		{
		}

		// {controller} GET
		public async Task<IActionResult> GetAllResources(string name)
		{
		}
	}
}
