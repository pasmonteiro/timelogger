namespace Timelogger.Api.DTOs
{
	public class ProjectDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string Deadline { get; set; }
		public int TotalMinutes { get; set; }
	}
}
