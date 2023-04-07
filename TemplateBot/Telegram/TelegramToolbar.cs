using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TemplateBot.Telegram
{
    internal class TelegramToolbar
    {
        private List<InlineKeyboardButton[]> keyboardButtons;

        public TelegramToolbar()
        {
            keyboardButtons = new List<InlineKeyboardButton[]>();
        }

        public void AddInlineKeyboardButton(String text, String callbackData)
        {
            keyboardButtons.Add(new[] { InlineKeyboardButton.WithCallbackData(text: text, callbackData: callbackData) });
        }

        public InlineKeyboardMarkup GetInlineKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(keyboardButtons);
        }
    }
}
