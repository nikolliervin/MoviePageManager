using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OpenAI
{
	public class ResponseBody
	{
		public string Id { get; set; }
		public string Object { get; set; }
		public int Created { get; set; }
		public string Model { get; set; }
		public List<Choice> Choices { get; set; }
		public Usage Usage { get; set; }
	}
}
