using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types;
using Telegram.Bot;
using TemplateBot.Models;

namespace TemplateBot.Telegram
{
    internal class TelegramPayment : TelegramSender
    {

        /// <summary>
        /// Собираем ответ для оплаты услуг;
        /// </summary>
        /// <param name = "botClient" > Передаем из HandleUpdateAsync</param>
        /// <param name = "update" > Передаем из HandleUpdateAsync</param>
        /// <param name = "cancellationToken" > Передаем из HandleUpdateAsync</param>
        public async Task ResponsCallbackPayment(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = new UpdateModel(update);

            var priceAmount = 500;

            List<LabeledPrice> prices = new List<LabeledPrice>() { };
            prices.Add(new LabeledPrice("PriceLable", priceAmount * 100)); //Сумма в копейках (*100)

            await SendInvoice(botClient, message.ChatId, "PriceTitle", "PriceDescription", "PaymentTokenLife", prices);
        }

        /// <summary>
        /// Собираем ответ для сообщения после оплаты услуг;
        /// Фиксируем оплату конкретного пользователя;
        /// </summary>
        /// <param name = "botClient" > Передаем из HandleUpdateAsync</param>
        /// <param name = "update" > Передаем из HandleUpdateAsync</param>
        /// <param name = "cancellationToken" > Передаем из HandleUpdateAsync</param>
        public async Task SuccessfulPayment(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = new UpdateModel(update);

            await SendTextMessage(botClient, message.ChatId, "Оплата успешно прошла!", cancellationToken);
        }
    }
}
