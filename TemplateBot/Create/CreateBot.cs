using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TemplateBot.Telegram;

namespace TemplateBot.Create
{
    internal class CreateBot
    {
        public TelegramBotClient BotClient;

        public CancellationTokenSource Cancellation;

        public CreateBot(String telegramToken)
        {
            BotClient = new TelegramBotClient(telegramToken);

            Cancellation = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            BotClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: Cancellation.Token);
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                var telegramController = new TelegramController();
                var telegramPaymment = new TelegramPayment();

                #region Message

                if (update.Type == UpdateType.Message)
                {
                    #region MessageText

                    if (update.Message!.Type == MessageType.Text)
                    {
                        await telegramController.ResponsMessage(botClient, update, cancellationToken);
                    }

                    #endregion

                    #region SuccessfulPayment

                    else if (update.Message!.Type == MessageType.SuccessfulPayment)
                    {
                        await telegramPaymment.SuccessfulPayment(botClient, update, cancellationToken);
                    }

                    #endregion
                }

                #endregion

                #region CallbackQuery

                if (update.Type == UpdateType.CallbackQuery)
                {
                    await telegramController.ResponsCallbackQuery(botClient, update, cancellationToken);
                }

                #endregion

                #region Telegram PreCheckoutQuery

                if (update.Type == UpdateType.PreCheckoutQuery)
                {
                    await botClient.AnswerPreCheckoutQueryAsync(update.PreCheckoutQuery!.Id);
                }

                #endregion
            }
            catch (Exception exp)
            {
                await botClient.AnswerCallbackQueryAsync(update.CallbackQuery!.Id, $"Error: {exp.Message}", true);
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
