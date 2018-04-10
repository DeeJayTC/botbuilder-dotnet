// Copyright (c) Teamwork.com. All rights reserved.
// Core Framework Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Extensions.Slack.Adapter;
using System;


namespace Microsoft.Bot.Builder.Core.Extensions.Slack
{
	/// <summary>
	/// Provides context for a turn of a bot.
	/// </summary>
	/// <remarks>Context provides information needed to process an incoming activity.
	/// The context object is created by a <see cref="SlackCommandAdapter"/> and persists for the 
	/// length of the turn.</remarks>
	/// <seealso cref="IBot"/>
	/// <seealso cref="IMiddleware"/>
	public class CommandContext : ICommandContext
	{
		private readonly SlackCommandAdapter _adapter;
		private bool _responded = false;
		private readonly Command _command;

		private readonly TurnContextServiceCollection _services = new TurnContextServiceCollection();

		/// <summary>
		/// Creates a context object.
		/// </summary>
		/// <param name="adapter">The adapter creating the context.</param>
		/// <param name="activity">The incoming activity for the turn;
		/// or <c>null</c> for a turn for a proactive message.</param>
		/// <exception cref="ArgumentNullException"><paramref name="activity"/> or
		/// <paramref name="adapter"/> is <c>null</c>.</exception>
		/// <remarks>For use by bot adapter implementations only.</remarks>
		public CommandContext(SlackCommandAdapter adapter, Command command)
		{
			_adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
			_command = command ?? throw new ArgumentNullException(nameof(command));
		}


		/// <summary>
		/// Gets the bot adapter that created this context object.
		/// </summary>
		public SlackCommandAdapter Adapter => _adapter;

		/// <summary>
		/// Gets the services registered on this context object.
		/// </summary>
		public ITurnContextServiceCollection Services => _services;

		/// <summary>
		/// Gets the activity associated with this turn; or <c>null</c> when processing
		/// a proactive message.
		/// </summary>
		public Command Command => _command;

		/// <summary>
		/// Indicates whether at least one response was sent for the current turn.
		/// </summary>
		/// <value><c>true</c> if at least one response was sent for the current turn.</value>
		/// <exception cref="ArgumentException">You attempted to set the value to <c>false</c>.</exception>
		public bool Responded
		{
			get { return _responded; }
			set
			{
				if (value == false)
				{
					throw new ArgumentException("CommandContext: cannot set 'responded' to a value of 'false'.");
				}
				_responded = true;
			}
		}

	}
}
