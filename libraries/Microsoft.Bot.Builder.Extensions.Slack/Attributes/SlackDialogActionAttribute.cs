using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Bot.Builder.Extensions.Slack.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class SlackDialogActionAttribute : Attribute
    {
		private string name;
		public SlackDialogActionAttribute(string name) => this.name = name;
	}
}
