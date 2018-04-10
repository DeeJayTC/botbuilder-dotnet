using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Extensions.Slack.Interfaces
{
    public interface ICommandHandler
	{
		/// <summary>
		/// Handles an incoming activity.
		/// </summary>
		/// <param name="commandContext">The context object for this turn.</param>
		/// <returns>A task that represents the work queued to execute.</returns>
		/// <remarks>The <paramref name="commandContext"/> provides information about the 
		/// incoming activity, and other data needed to process the activity.</remarks>
		/// <seealso cref="ICommand"/>
		/// <seealso cref="Bot.Schema.ICommandContext"/>
		Task OnTurn(ICommand commandContext);
	}
}
