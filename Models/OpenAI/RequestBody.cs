using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OpenAI
{
	public class RequestBody
	{
		public string model { get; set; }
		public string prompt { get; set; }
		public int max_tokens { get; set; }
		public int temperature { get; set; }
		public int top_p { get; set; }
		public int n { get; set; }
		public bool stream { get; set; }
		public object logprobs { get; set; }
		public string stop { get; set; }

	}
}
