using Microsoft.Bot.Builder.Extensions.Slack.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Bot.Builder
{
	public static class BotAssertExtension
    {
		/// <summary>
		/// Checks that an activity object is not <c>null</c>.
		/// </summary>
		/// <param name="activity">The activity object.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="activity"/> is <c>null</c>.</exception>
		public static void CommandNotNull(ICommand activity)
		{
			if (activity == null)
				throw new ArgumentNullException(nameof(activity));
		}
	}
}
