using Microsoft.Bot.Builder.Core.Extensions.Slack;
using Microsoft.Bot.Builder.Extensions.Slack.Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Builder.Extensions.Slack.Schema;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Rest.TransientFaultHandling;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Extensions.Slack.Adapter
{
    public abstract class SlackAdapterBase
    {



		/// <summary>
		/// The collection of middleware in the adapter's pipeline.
		/// </summary>
		protected readonly MiddlewareSetSlack _middlewareSet = new MiddlewareSetSlack();

		/// <summary>
		/// Creates a default adapter.
		/// </summary>
		public SlackAdapterBase() : base()
        {
		}

		/// <summary>
		/// Adds middleware to the adapter's pipeline.
		/// </summary>
		/// <param name="middleware">The middleware to add.</param>
		/// <returns>The updated adapter object.</returns>
		/// <remarks>Middleware is added to the adapter at initialization time.
		/// For each turn, the adapter calls middleware in the order in which you added it.
		/// </remarks>
		public SlackAdapterBase Use(IMiddlewareSlack middleware)
		{
			_middlewareSet.Use(middleware);
			return this;
		}


		protected async Task RunPipeline(ICommandContext context, Func<ICommandContext, Task> callback = null, CancellationTokenSource cancelToken = null)
		{
			BotAssertSlack.ContextNotNull(context);

			// Call any registered Middleware Components looking for ReceiveActivity()
			if (context.Command != null)
			{
				await _middlewareSet.ReceiveActivityWithStatus(context, callback).ConfigureAwait(false);
			}
			else
			{
				// call back to caller on proactive case
				if (callback != null)
				{
					await callback(context).ConfigureAwait(false);
				}
			}
		}
	}
}
