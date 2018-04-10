using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder.Extensions.Slack.Adapter;
using Microsoft.Bot.Builder.Extensions.Slack.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder.Extensions.Slack.Handlers
{
	public class CommandMessageHandler : CommandMessageHandlerBase
	{
		public CommandMessageHandler(SlackCommandAdapter slackCommandAdapter) : base (slackCommandAdapter)
        {
		}

		protected override async Task ProcessMessageRequestAsync(HttpRequest request, SlackCommandAdapter slackCommandAdapter, Func<ITurnContext, Task> botCallbackHandler)
		{
			var command = default(Command);

			using (var bodyReader = new JsonTextReader(new StreamReader(request.Body, Encoding.UTF8)))
			{
				command = CommandMessageHandlerBase.BotMessageSerializer.Deserialize<Command>(bodyReader);
			}

			await slackCommandAdapter.ProcessCommand(
					request.Headers["Authorization"],
					command,
					botCallbackHandler);
		}


	}
}
