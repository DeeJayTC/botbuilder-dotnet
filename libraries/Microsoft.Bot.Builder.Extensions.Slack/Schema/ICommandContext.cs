using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Extensions.Slack.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Bot.Builder.Core.Extensions.Slack
{
	public interface ICommandContext
	{
		/// <summary>
		/// Gets the bot adapter that created this context object.
		/// </summary>
		SlackCommandAdapter Adapter { get; }

		/// <summary>
		/// Gets the services registered on this context object.
		/// </summary>
		ITurnContextServiceCollection Services { get; }

		/// <summary>
		/// Gets the activity associated with this turn; or <c>null</c> when processing
		/// a proactive message.
		/// </summary>
		Command Command { get; }
	}
}
