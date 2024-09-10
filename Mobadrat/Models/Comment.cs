using System.ComponentModel.DataAnnotations;

namespace Mobadrat.Models
{
	public class Comment
	{
		[Key]
		public int MobadraCommentID { get; set; }
		public string MobadraCommentName { get; set; }
	
	}
}
