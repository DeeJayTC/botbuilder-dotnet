using Microsoft.Bot.Connector.Authentication;
using Microsoft.Rest.TransientFaultHandling;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Extensions.Slack.Schema;
using Microsoft.Bot.Builder.Extensions.Slack.Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Connector;

namespace Microsoft.Bot.Builder.Extensions.Slack.Adapter
{
    public class SlackCommandAdapter : SlackAdapterBase
	{
		private readonly ICredentialProvider _credentialProvider;
		private readonly HttpClient _httpClient;
		private readonly RetryPolicy _connectorClientRetryPolicy;
		private Dictionary<string, MicrosoftAppCredentials> _appCredentialMap = new Dictionary<string, MicrosoftAppCredentials>();


		public SlackCommandAdapter(ICredentialProvider credentialProvider, RetryPolicy connectorClientRetryPolicy = null, HttpClient httpClient = null, IMiddleware middleware = null)
        {
            _credentialProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
			_httpClient = httpClient ?? new HttpClient();
			_connectorClientRetryPolicy = connectorClientRetryPolicy;

		}


		public async Task ProcessActivity(string authHeader, Command command, Func<ICommandContext, Task> callback)
		{
			BotAssertExtension.CommandNotNull(command);
			var claimsIdentity = await JwtTokenValidationSlack.AuthenticateRequest(command, authHeader, _credentialProvider, _httpClient);

			var context = new TurnContext(this, activity);
			context.Services.Add<IIdentity>("BotIdentity", claimsIdentity);
			var connectorClient = await this.CreateConnectorClientAsync(activity.ServiceUrl, claimsIdentity);
			context.Services.Add<IConnectorClient>(connectorClient);
			await base.RunPipeline(context, callback).ConfigureAwait(false);
		}


		public override Task DeleteActivity(ITurnContext context, ConversationReference reference)
		{
			throw new NotImplementedException();
		}

		public override Task<ResourceResponse[]> SendActivities(ITurnContext context, Activity[] activities)
		{
			throw new NotImplementedException();
		}

		public override Task<ResourceResponse> UpdateActivity(ITurnContext context, Activity activity)
		{
			throw new NotImplementedException();
		}
	}
}
