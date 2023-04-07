using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using TemplateBot.Models;

namespace TemplateBot.Telegram
{
    internal class TelegramController : TelegramSender
    {

        /// <summary>
        /// Собираем ответ для стартового сообщения;
        /// Добавляем нового пользователя в базу;
        /// Фиксируем ответ на вопрос конкретного пользователя;
        /// </summary>
        /// <param name="botClient">Передаем из HandleUpdateAsync</param>
        /// <param name="update">Передаем из HandleUpdateAsync</param>
        /// <param name="cancellationToken">Передаем из HandleUpdateAsync</param>
        public async Task ResponsMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = new UpdateModel(update);

            Console.WriteLine($"Получено сообщение: '{message.MessageText}' в чате {message.ChatId} от {message.Username} ({message.FirstName} {message.SecondName})");

            if (message.MessageText == "/test")
            {
                #region Testing

                #endregion
            }

            if (message.MessageText == "/start")
            {
                var toolbar = new TelegramToolbar();

                toolbar.AddInlineKeyboardButton("Button1", "Value1");
                toolbar.AddInlineKeyboardButton("Button2", "Value2");

                var replyMarkup = toolbar.GetInlineKeyboardMarkup();

                await SendTextMessage(botClient, message.ChatId, "Test text", replyMarkup, cancellationToken);
            }
        }

        /// <summary>
        /// Собираем ответ для изменения сообщения;
        /// Фиксируем ответ на вопрос конкретного пользователя;
        /// </summary>
        /// <param name="botClient">Передаем из HandleUpdateAsync</param>
        /// <param name="update">Передаем из HandleUpdateAsync</param>
        /// <param name="cancellationToken">Передаем из HandleUpdateAsync</param>
        public async Task ResponsCallbackQuery(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = new UpdateModel(update);

            if (message.ButtonData == "Value1")
            {
                await SendTextMessage(botClient, message.ChatId, "Вы нажали на Button 1", cancellationToken);
            }
            if (message.ButtonData == "Value2")
            {
                await EditMessageText(botClient, message.ChatId, message.MessageId, "Вы нажали на Button 2", cancellationToken);
            }
        }
    }
}
