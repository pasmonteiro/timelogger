using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public class Project
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Deadline { get; set; }
		public ProjectStatus Status { get; set; }

		public virtual List<Entry> Entries { get; set; }
	}

	public enum ProjectStatus
	{
		NotInitiated = 0,
		InProgress = 1,
		Completed = 2
	}
}
