﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.OpenAI
{
	public class Usage
	{
		public int PromptTokens { get; set; }
		public int CompletionTokens { get; set; }
		public int TotalTokens { get; set; }
	}
}
