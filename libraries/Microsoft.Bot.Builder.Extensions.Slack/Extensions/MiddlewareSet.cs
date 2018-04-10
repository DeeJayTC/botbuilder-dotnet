// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder.Core.Extensions.Slack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder
{
    public class MiddlewareSetSlack : IMiddlewareSlack
    {
        public delegate Task NextDelegate();

        private readonly IList<IMiddlewareSlack> _middleware = new List<IMiddlewareSlack>();

        public MiddlewareSetSlack Use(IMiddlewareSlack middleware)
        {
            BotAssertSlack.MiddlewareNotNull(middleware);
            _middleware.Add(middleware);
            return this;
        }

        public async Task ReceiveActivity(ICommandContext context)
        {
            await ReceiveActivityInternal(context, null).ConfigureAwait(false);
        }

        public async Task OnTurn(ICommandContext context, NextDelegate next)
        {
            await ReceiveActivityInternal(context, null).ConfigureAwait(false);
            await next().ConfigureAwait(false);
        }

        /// <summary>
        /// Intended to be called from Bot, this method performs exactly the same as the
        /// standard ReceiveActivity, except that it runs a user-defined delegate returns 
        /// if all Middlware in the receive pipeline was run.
        /// </summary>
        public async Task ReceiveActivityWithStatus(ICommandContext context, Func<ICommandContext, Task> callback)
        {
            await ReceiveActivityInternal(context, callback).ConfigureAwait(false);
        }

        private Task ReceiveActivityInternal(ICommandContext context, Func<ICommandContext, Task> callback, int nextMiddlewareIndex = 0)
        {
            // Check if we're at the end of the middleware list yet
            if(nextMiddlewareIndex == _middleware.Count)
            {
                // If all the Middlware ran, the "leading edge" of the tree is now complete. 
                // This means it's time to run any developer specified callback. 
                // Once this callback is done, the "trailing edge" calls are then completed. This
                // allows code that looks like:
                //      Trace.TraceInformation("before");
                //      await next();
                //      Trace.TraceInformation("after"); 
                // to run as expected.

                // If a callback was provided invoke it now and return its task, otherwise just return the completed task
                return callback?.Invoke(context) ?? Task.CompletedTask;
            }

            // Get the next piece of middleware 
            var nextMiddleware = _middleware[nextMiddlewareIndex];


            // Execute the next middleware passing a closure that will recurse back into this method at the next piece of middlware as the NextDelegate
            return nextMiddleware.OnTurn(
                context,
                () => ReceiveActivityInternal(context, callback, nextMiddlewareIndex + 1));
        }

	}
}
