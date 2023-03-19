using System.ComponentModel.DataAnnotations.Schema;

namespace Timelogger.Entities
{
	public class Entry
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public int Minutes { get; set; }
		public int ProjectId { get; set; }

		[ForeignKey("ProjectId")]
		public virtual Project Project { get; set; }
	}
}
