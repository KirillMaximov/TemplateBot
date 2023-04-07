using Telegram.Bot;
using TemplateBot.Create;

#region Create bots

var telegramToken = "telegramToken";
CreateBot bot = new CreateBot(telegramToken);
var botName = await bot.BotClient.GetMeAsync();
Console.WriteLine($"Начинаем работу с @{botName.Username}");

#endregion

#region Задержка для работы консоли

await Task.Delay(int.MaxValue);

#endregion

#region Удаление токена после работы

bot.Cancellation.Cancel();

#endregion