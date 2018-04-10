using Microsoft.Bot.Builder.Core.Extensions.Slack;
using Microsoft.Bot.Schema;
using System;

namespace Microsoft.Bot.Builder
{
	public static class BotAssertSlack
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
		/// <summary>
		/// Checks that an activity object is not <c>null</c>.
		/// </summary>
		/// <param name="activity">The activity object.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="activity"/> is <c>null</c>.</exception>
		public static void ContextNotNull(ICommandContext activity)
		{
			if (activity == null)
				throw new ArgumentNullException(nameof(activity));
		}

		/// <summary>
		/// Checks that an middleware object is not <c>null</c>.
		/// </summary>
		/// <param name="activity">The activity object.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="activity"/> is <c>null</c>.</exception>
		public static void MiddlewareNotNull(IMiddlewareSlack activity)
		{
			if (activity == null)
				throw new ArgumentNullException(nameof(activity));
		}

	}
}
