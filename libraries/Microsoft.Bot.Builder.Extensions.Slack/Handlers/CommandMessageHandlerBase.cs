using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder.Extensions.Slack.Adapter;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Extensions.Slack
{
	public abstract class CommandMessageHandlerBase
	{
		public static readonly JsonSerializer BotMessageSerializer = JsonSerializer.Create(new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			Formatting = Newtonsoft.Json.Formatting.Indented,
			NullValueHandling = NullValueHandling.Ignore,
		});

		private SlackCommandAdapter _slackCommandAdapter;

		public CommandMessageHandlerBase(SlackCommandAdapter slackCommandAdapter)
		{
			_slackCommandAdapter = slackCommandAdapter;
		}

		public async Task HandleAsync(HttpContext httpContext)
		{
			var request = httpContext.Request;
			var response = httpContext.Response;

			if (request.Method != HttpMethods.Post)
			{
				response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;

				return;
			}

			if (request.ContentLength == 0)
			{
				response.StatusCode = (int)HttpStatusCode.BadRequest;

				return;
			}

			if (!MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeaderValue)
				||
				(mediaTypeHeaderValue.MediaType != "application/json" && mediaTypeHeaderValue.MediaType != "application/x-www-form-urlencoded")
				)
			{
				response.StatusCode = (int)HttpStatusCode.NotAcceptable;

				return;
			}

			try
			{
				await ProcessMessageRequestAsync(
					request,
					_botFrameworkAdapter,
					context =>
					{
						var bot = httpContext.RequestServices.GetRequiredService<IBot>();

						return bot.OnTurn(context);
					});

				response.StatusCode = (int)HttpStatusCode.OK;
			}
			catch (UnauthorizedAccessException)
			{
				response.StatusCode = (int)HttpStatusCode.Forbidden;
			}
		}

		protected abstract Task ProcessMessageRequestAsync(HttpRequest request, SlackCommandAdapter slackCommandAdapter, Func<ITurnContext, Task> botCallbackHandler));
	}
}
