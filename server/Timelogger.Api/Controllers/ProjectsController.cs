using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Timelogger.Api.DTOs;
using Timelogger.Entities;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	public class ProjectsController : Controller
	{
		private readonly ILogger<ProjectsController> _logger;
		private readonly ApiContext _context;

		public ProjectsController(ILogger<ProjectsController> logger, ApiContext context)
		{
			_logger = logger;
			_context = context;
		}

		// GET api/projects
		[HttpGet]
		public IActionResult Index([FromQuery] string search)
		{
			var lowerSearch = search?.ToLowerInvariant();
			Expression<Func<Project, bool>> filter = _ => string.IsNullOrEmpty(lowerSearch) || _.Name.ToLowerInvariant().IndexOf(lowerSearch) != -1;
			var count = _context.Projects.Count(filter);
			var items = _context.Projects.Include(_ => _.Entries).Where(filter).Select(_ => new ProjectDTO
			{
				Id = _.Id,
				Name = _.Name,
				Status = _.Status.ToString(),
				TotalMinutes = _.Entries.Sum(_ => _.Minutes),
				Deadline = _.Deadline.ToShortDateString()
			}).ToList();
			
			return Ok(new { items, count });
		}

		// GET api/projects/{id}
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			Project project;
			try
			{
				project = _context.Projects.Include(_ => _.Entries).First(_ => _.Id == id);
			}
			catch (System.Exception ex)
			{
				var message = $"While getting product with id {id}.";
				_logger.LogError(ex, message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}

			if (project is null)
			{
				var message = $"Product with id {id} was not found.";
				_logger.LogError(message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}

			var entries = project.Entries.Select(_ => new EntryDTO
			{
				Id = _.Id,
				Description = _.Description,
				Minutes = _.Minutes
			});

			return Ok(new
			{
				success = true,
				entries,
				project = new ProjectDTO
				{
					Id = project.Id,
					Name = project.Name,
					Status = project.Status.ToString(),
					TotalMinutes = project.Entries.Sum(_ => _.Minutes),
					Deadline = project.Deadline.ToShortDateString()
				}
			});
		}

		// POST api/projects/addEntry
		[HttpPost]
		[Route("addEntry")]
		public IActionResult AddEntry([FromBody] EntryDTO dto)
		{
			if (dto.Minutes < 30)
			{
				var message = $"Minutes must be greater than 30 minutes.";
				_logger.LogError(message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}

			Project project;
			try
			{
				project = _context.Projects.Find(dto.ProjectId);
			}
			catch (System.Exception ex)
			{
				var message = $"While getting product with id {dto.ProjectId}.";
				_logger.LogError(ex, message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}

			if (project is null)
			{
				var message = $"Product with id {dto.ProjectId} was not found.";
				_logger.LogError(message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}

			if (project.Status == ProjectStatus.Completed)
			{
				var message = $"Product with id {dto.ProjectId} is completed.";
				_logger.LogError(message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}

			var entry = new Entry
			{
				Description = dto.Description,
				Minutes = dto.Minutes,
				ProjectId = dto.ProjectId
			};

			try
			{
				_context.Entries.Add(entry);
				_context.SaveChanges();
				return Ok(new
				{
					success = true
				});
			}
			catch (System.Exception ex)
			{
				var message = "Unable to save entry.";
				_logger.LogError(ex, message);
				return Ok(new
				{
					success = false,
					errors = new string[] { message }
				});
			}
		}
	}
}
