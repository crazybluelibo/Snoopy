using System; 
namespace Snoopy.Data.Model
{
	public class checkup
	{
		public int ID{ get; set; }

		public string user_id{ get; set; }

		public string check_desc{ get; set; }

		public DateTime plan_date{ get; set; }

		public DateTime act_date{ get; set; }

		public int weeks{ get; set; }

		public int months{ get; set; }

		public int status{ get; set; }

	}
}