using Microsoft.AspNetCore.Builder;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.Integration.AspNet.Core.Handlers;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Bot.Builder.Extensions.Slack.Adapter;
using Microsoft.Bot.Builder.Extensions.Slack.Handlers;

namespace Microsoft.Bot.Builder.Extensions.Slack
{
	/// <summary>
	/// Extension methods for <see cref="IApplicationBuilder"/> to add a Bot to the ASP.NET Core request execution pipeline.
	/// </summary>
	/// <seealso cref="BotFrameworkPaths"/>
	/// <seealso cref="BotFrameworkAdapter"/>
	/// <seealso cref="ServiceCollectionExtensions"/>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Maps various endpoint handlers for the <see cref="ServiceCollectionExtensions.AddBot{TBot}(IServiceCollection, Action{BotFrameworkOptions})">registered bot</see> into the request execution pipeline.
		/// </summary>
		/// <param name="appicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
		/// <param name="configurePaths">A callback to configure the paths that determine where the endpoints of the bot will be exposed.</param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		/// <seealso cref="ServiceCollectionExtensions.AddBot{TBot}(IServiceCollection, Action{BotFrameworkOptions})"/>
		/// <seealso cref="BotFrameworkPaths"/>
		public static IApplicationBuilder UseBotFrameworkSlack(this IApplicationBuilder applicationBuilder)
		{

			if (applicationBuilder == null)
			{
				throw new ArgumentNullException(nameof(applicationBuilder));
			}


			var options = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;


			var paths = new BotFrameworkPathsSlack();
			configurePaths(paths);

			var slackCommandAdapter = new SlackCommandAdapter(options.CredentialProvider, options.ConnectorClientRetryPolicy);

			applicationBuilder.Map(
				paths.BasePath + paths.SlashCommandsPath,
				botActivitiesAppBuilder => botActivitiesAppBuilder.Run(new CommandMessageHandler(slackCommandAdapter).HandleAsync));

			return applicationBuilder;


		}

		/// <summary>
		/// Maps various endpoint handlers for the <see cref="ServiceCollectionExtensions.AddBot{TBot}(IServiceCollection, Action{BotFrameworkOptions})">registered bot</see> into the request execution pipeline.
		/// </summary>
		/// <param name="appicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
		/// <param name="configurePaths">A callback to configure the paths that determine where the endpoints of the bot will be exposed.</param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		/// <seealso cref="ServiceCollectionExtensions.AddBot{TBot}(IServiceCollection, Action{BotFrameworkOptions})"/>
		/// <seealso cref="BotFrameworkPaths"/>
		public static IApplicationBuilder UseBotFrameworkSlack(this IApplicationBuilder applicationBuilder, Action<BotFrameworkPathsSlack> configurePaths)
		{

			if (applicationBuilder == null)
			{
				throw new ArgumentNullException(nameof(applicationBuilder));
			}

			if (configurePaths == null)
			{
				throw new ArgumentNullException(nameof(configurePaths));
			}

			var options = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;


			var paths = new BotFrameworkPathsSlack();
			configurePaths(paths);

			var slackCommandAdapter = new SlackCommandAdapter(options.CredentialProvider, options.ConnectorClientRetryPolicy);

			applicationBuilder.Map(
				paths.BasePath + paths.SlashCommandsPath,
				botActivitiesAppBuilder => botActivitiesAppBuilder.Run(new CommandMessageHandler(slackCommandAdapter).HandleAsync));

			return applicationBuilder;


		}
	}
}
