using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OpenAI
{
	public class Choice
	{
		public string Text { get; set; }
		public int Index { get; set; }
		public object Logprobs { get; set; }
		public string FinishReason { get; set; }
	}
}
