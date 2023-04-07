using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TemplateBot.Telegram
{
    internal class TelegramSender
    {
        /// <summary>
        /// Отправка нового сообщения в телеграм;
        /// </summary>
        /// <param name="chatId">Номер чата в который отправляем сообщение</param>
        /// <param name="messageText">Текст сообщения</param>
        /// <param name="replyMarkup">Меню сообщения</param>
        /// <returns></returns>
        protected async Task SendTextMessage(
            ITelegramBotClient botClient,
            long chatId,
            string messageText,
            IReplyMarkup replyMarkup,
            CancellationToken cancellationToken)
        {
            Message sendMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: messageText,
                replyMarkup: replyMarkup,
                cancellationToken: cancellationToken); //parseMode: ParseMode.Html (для добавления html текста с форматированием)
        }

        protected async Task SendTextMessage(
            ITelegramBotClient botClient,
            long chatId,
            string messageText,
            CancellationToken cancellationToken)
        {
            Message sendMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: messageText,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Отправка измененного сообщения в телеграм взамен текущему;
        /// </summary>
        /// <param name="chatId">Номер чата в котором изменяем сообщение</param>
        /// <param name="messageId">Номер сообщения которые будем изменять</param>
        /// <param name="messageText">Текст измененного сообщения</param>
        /// <param name="replyMarkup">Меню измененного сообщения</param>
        /// <returns></returns>
        protected async Task EditMessageText(
            ITelegramBotClient botClient,
            long chatId,
            int messageId,
            string messageText,
            InlineKeyboardMarkup replyMarkup,
            CancellationToken cancellationToken)
        {
            Message sendMessage = await botClient.EditMessageTextAsync(
                chatId: chatId,
                messageId: messageId,
                text: messageText,
                replyMarkup: replyMarkup,
                cancellationToken: cancellationToken);
        }

        protected async Task EditMessageText(
            ITelegramBotClient botClient,
            long chatId,
            int messageId,
            string messageText,
            CancellationToken cancellationToken)
        {
            Message sendMessage = await botClient.EditMessageTextAsync(
                chatId: chatId,
                messageId: messageId,
                text: messageText,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Отправка ссылки на оплату услуг;
        /// </summary>
        /// <param name="chatId">Номер чата в который отправляем сообщение</param>
        /// <param name="title">Гллавление оплаты</param>
        /// <param name="description">Описание оплаты</param>
        /// <param name="token">Ключ в системе платежей</param>
        /// <param name="prices">List<LabeledPrice> список цен на услуги</param>
        /// <param name="currency">Валюта для оплаты</param>
        /// <returns></returns>
        protected async Task SendInvoice(
            ITelegramBotClient botClient,
            long chatId,
            string title,
            string description,
            string token,
            List<LabeledPrice> prices,
            string currency = "RUB")
        {
            //https://core.telegram.org/bots/payments#supported-currencies
            Message sendMessage = await botClient.SendInvoiceAsync(
                chatId: chatId,
                title: title,
                description: description,
                providerToken: token,
                currency: currency,
                prices: prices,
                startParameter: "payment_for_services",
                payload: "test_invoice_payload");
        }

        protected async Task SendDocument(
            ITelegramBotClient botClient,
            long chatId,
            string sourceFile,
            string telegramName,
            string caption)
        {
            using (var stream = System.IO.File.OpenRead(sourceFile))
            {
                InputOnlineFile documentFile = new InputOnlineFile(stream);
                documentFile.FileName = telegramName;

                Message sendMessage = await botClient.SendDocumentAsync(
                    chatId: chatId,
                    document: documentFile,
                    caption: caption);
            }
        }
    }
}
