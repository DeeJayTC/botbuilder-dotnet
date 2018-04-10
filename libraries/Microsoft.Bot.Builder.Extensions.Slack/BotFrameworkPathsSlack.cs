using Microsoft.AspNetCore.Http;

namespace Microsoft.Bot.Builder.Extensions.Slack
{
    public class BotFrameworkPathsSlack 
    {
		public BotFrameworkPathsSlack()
		{
			this.BasePath = "/api";
			this.SlashCommandsPath = "/commands";
		}

		/// <summary>
		/// Gets or sets the base path at which the bot's endpoints should be exposed.
		/// </summary>
		/// <value>
		/// A <see cref="PathString"/> that represents the base URL at which the bot should be exposed.
		/// </value>
		public PathString BasePath { get; set; }

		/// <summary>
		/// Gets or sets the path, relative to the <see cref="BasePath"/>, at which the bot framework messages are expected to be delivered.
		/// </summary>
		/// <value>
		/// A <see cref="PathString"/> that represents the URL at which the bot framework messages are expected to be delivered.
		/// </value>
		public PathString SlashCommandsPath { get; set; }
	}
}