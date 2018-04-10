using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Bot.Builder.Extensions.Slack.Attributes
{

	[AttributeUsage(AttributeTargets.Method)]
	public class SlackCommandAttribute : Attribute
    {
		private string name;
		public SlackCommandAttribute(string name) => this.name = name;
	}
}
