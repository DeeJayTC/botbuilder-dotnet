using Microsoft.Bot.Builder.Extensions.Slack.Interfaces;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Bot.Builder.Extensions.Slack.Extensions
{
	/// <summary>
	/// Extension class for bot integration with ASP.NET Core 2.0 projects.
	/// </summary>
	/// <seealso cref="ApplicationBuilderExtensions"/>
	/// <seealso cref="BotAdapter"/>
	public static class ServiceCollectionExtensions
	{
		private static readonly JsonSerializer ActivitySerializer = JsonSerializer.Create();


		/// <summary>
		/// Adds and configures services for a <typeparamref name="TCommandHandler">specified bot type</typeparamref> to the <see cref="IServiceCollection" />.
		/// </summary>
		/// <typeparam name="TCommandHandler">A concrete type of <see cref="ICommandHandler"/ > that is to be registered and exposed to the Bot Framework.</typeparam>
		/// <param name="services">The <see cref="IServiceCollection"/>.</param>
		/// <param name="configureAction">A callback that can further be used to configure the bot.</param>
		/// <returns>A reference to this instance after the operation has completed.</returns>
		public static IServiceCollection AddCommandBot<TCommandHandler>(this IServiceCollection services, Action<BotFrameworkOptions> configureAction = null) where TCommandHandler : class, ICommandHandler
		{
			services.AddTransient<ICommandHandler, TCommandHandler>();

			services.Configure(configureAction);

			return services;
		}
	}
}
