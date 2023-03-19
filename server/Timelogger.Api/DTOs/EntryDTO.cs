namespace Timelogger.Api.DTOs
{
	public class EntryDTO
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public int Minutes { get; set; }
		public int ProjectId { get; set; }
	}
}
