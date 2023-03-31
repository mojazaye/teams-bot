using MyTeamsApp.Models;
using AdaptiveCards.Templating;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.TeamsFx.Conversation;
using Newtonsoft.Json;

namespace MyTeamsApp.Commands
{
    /// <summary>
    /// The <see cref="HelloWorldCommandHandler"/> registers a pattern with the <see cref="ITeamsCommandHandler"/> and 
    /// responds with an Adaptive Card if the user types the <see cref="TriggerPatterns"/>.
    /// </summary>
    public class HelloWorldCommandHandler : ITeamsCommandHandler
    {
        private readonly ILogger<HelloWorldCommandHandler> _logger;
        private readonly string _adaptiveCardFilePath = Path.Combine(".", "Resources", "HelloWorldCard.json");
        private CancellationToken _ccToken;
        public IEnumerable<ITriggerPattern> TriggerPatterns => new List<ITriggerPattern>
        {
            // Used to trigger the command handler if the command text contains 'helloworld'
            new RegExpTrigger("jeff teper")
        };

        public HelloWorldCommandHandler(ILogger<HelloWorldCommandHandler> logger)
        {
            _logger = logger;
            _ccToken = new CancellationToken(false);
        }

        public async Task<ICommandResponse> HandleCommandAsync(ITurnContext turnContext, CommandMessage message, CancellationToken cancellationToken = default)
        {
            _logger?.LogInformation($"Bot received message: {message.Text}");

            // Read adaptive card template
            var cardTemplate = await File.ReadAllTextAsync(_adaptiveCardFilePath, _ccToken);

            // Render adaptive card content
            var cardContent = new AdaptiveCardTemplate(cardTemplate).Expand
            (
                new HelloWorldModel
                {
                    Title = "This is a simulated streaming bot",
                    Body = "Congratulations! Your bot simulator is running. Click the documentation below to learn more about Bots and the Teams Toolkit.",
                }
            );

            // Build attachment
            var activity = MessageFactory.Attachment
            (
                new Attachment
                {
                    ContentType = "application/vnd.microsoft.card.adaptive",
                    Content = JsonConvert.DeserializeObject(cardContent),
                }
            );

            await turnContext.SendActivityAsync(activity, _ccToken).ConfigureAwait(false);
            // Send response
            await StreamingUpdate(activity, turnContext, _ccToken);

            return (ICommandResponse)await turnContext.UpdateActivityAsync(activity, _ccToken).ConfigureAwait(false);
        }

        private async Task StreamingUpdate(
            IMessageActivity activity,
            ITurnContext turnContext,
            CancellationToken cancellationToken = default)
        {
            try
            {
                for (int i = 0; i < 33; i++)
                {
                    string cardContent = HelloWorldModel.GetSampleCard(i);
                    activity.Attachments[0].Content = JsonConvert.DeserializeObject(cardContent);
                    await turnContext.UpdateActivityAsync(activity, cancellationToken).ConfigureAwait(false);

                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return;
        }
    }
}
