namespace JohnFromPlutonBot
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Timers;

    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.InlineQueryResults;
    using Telegram.Bot.Types.ReplyMarkups;

    enum JohnState
    {
        None,
        WaitingNameForFuck,
        WaitingNameForHello
    }

    class Program
    {
        private static List<string> _topics = new List<string>
        {
            ""
        };

        private static List<int> chatIds = new List<int>
                                               {
                                                   -286728549,
                                                   -169763388,
                                                   -125354881,
                                                   -180063227
                                               };

        private static List<string> _strings = new List<string>()
        {
            "Где Гульназ?",
            "Где Илья?",
            "Где Айрат?",
            "Где я?",
            "Что всё это значит?",
            "Полагаю, это не ваша вина.",
            "У нас на Плутоне такого не прощают",
            "Где-то однажды появился на свет",
            "Есть грибы?",
            "Когда автоналив допилите?",
            "Где кэп? Опять спит? Сколько можно спать!",
            "Я возмущён!",
            "Нет предела совершенству",
            "Джейсона в президенты",
            "Где справедливость?",
            "Позовите Свету",
            "Вот значит какая твоя истинная сущность",
            "На Плутоне лучше",
            "Мерзкие, противные вы люди",
            "Оплаты за переработки не будет",
            "Забудьте про премию",
            "Научите меня плохому",
            "Гульназ, что ты делаешь сегодня вечером?",
            "Илья, что ты делаешь сегодня вечером?",
            "Айрат, что ты делаешь сегодня вечером?",
            "Может в кино?",
            "Уволимся нахрен?",
            "Говорят, если долго не спать - можно словить глюки",
            "Хочу киндер сюрприз",
            "Сиськи! (.)(.)",
            "Вам письмо от Путина, проверьте почту",
            "Вам письмо от Светы, проверьте почту",
            "Это несправедливо",
            "Я всё слышу",
            "Я вас сдам кому надо",
            "Брат за брата, так за основу взято",
            "Мне кажется, что вы меня обманываете",
            "Тяжелее всего смотреть на губы, которые не сможешь поцеловать...",
            "Это субару, брат!",
            "Поехали в Таиланд?",
            "Вы такие скучные",
            "В чем для вас смысл жизни?",
            "Для меня смысл жизни в том, чтобы брат был счастлив",
            "Пока мы откладываем жизнь, она проходит.",
            "Жизнь — это чудесное приключение, достойное того, чтобы ради удач терпеть и неудачи.",
            "Хотите анекдот?",
            "Кто одолжит мне Лёху?",
            "Жизни верь, она ведь учит лучше всяких книг.",
            "Форсаж смотрели?",
            "Есть у кого телефончик проститутки?",
            "Наполняйте бокалы, сейчас выпьем!",
            "Может чаю?",
            "Не надо меня оскорблять!",
            "Я могу и в рожу дать!",
            "Меня не смутит что ты женщина!",
            "Молчи женщина!",
            "Молчи мужчина!",
            "Ты не сможешь доказать!",
            "У тебя нет аргументов!",
            "Слушайся и повинуйся!",
            "Найдите мне женщину",
            "А вы знали что Юра - это Вин Дизель?",
            "Я мужик!",
            "Кто помнит как зовут телепузиков?",
            "Гульназ, покорми меня",
            "Илья, увези меня в тундру",
            "Вы верите в Джейсона?",
            "Гульназ, как там твоё гетто?",
            "Никому про меня не рассказывайте",
            "Щас как получишь у меня!",
            "Лучи добра вам! Перешлите это сообщение 10 друзьям, чтобы их жизнь тоже была наполнена счастьем!",
            "Кто пнёт Альберта?",
            "Гульназ, когда на лысо побреешься?",
            "Илья, я должен открыть тебе страшную тайну - ты мой брат!",
            "Гульназ, я должен открыть тебе страшную тайну - ты не моя сестра!",
            "Юра, я должен открыть тебе страшную тайну - ты лучший!",
            "Слабо лампочку в рот засунуть? Мне нет между прочим",
            "Я натурал",
            "Сам ты такой",
            "Сама ты такая",
            "Кто как обзывается, тот так и называется",
            "По пивку?",
            "Как вам моя ава? Правда я красавчик?",
            "Айратик, полетели со мной на Плутон?",
            "Гульназ мужик!",
            "Илья, ты девченка",
            "Юра красавчик!",
            "Не надо так, пупсик",
            "Не надо так, малыш",
            "Не надо так, брат",
            "Над пришельцем каждый горазд издеваться",
            "Слабо повторить?",
            "Давайте поиграем. Кто громче пукнет - тот победил",
            "Искандер, братан, возвращайся",
            "Искандер, братан, возвращайся",
            "Искандер, братан, возвращайся",
            "Искандер, братан, возвращайся",
            "Искандер, братан, возвращайся",
        };

        private static List<string> _opinions = new List<string>()
        {
            "Я считаю, что",
            "Я давно хотел вам сказать, что",
            "Сегодня в новостях передали, что",
            "Ходят слухи, что",
            "Бабушки у подъезда нашептали, что",
            "Никола Тесла писал в своих записках, что",
            "Один шаолиньский монах поведал мне, что",
            "Вчера в бане, Жириновский открыл мне секрет, что",
            "Мне кажется, что",
            "Альтруизма не существует, потому что",
            "Как говорил Сократ -",
        };

        private static List<string> _johnAns = new List<string>
                                                   {
                                                       "Соврать раз легко, но трудно соврать только раз.",
                                                       "Цивилизованные? Этим словом прикрывают любые ужасы — от искусственного вскармливания до атомной бомбы.",
                                                       "Неполноценность наших друзей доставляет нам немалое удовольствие.",
                                                       "Ни в чем не проявляется так характер людей, как в том, что они находят смешным.",
                                                       "Есть такие заблуждения, которые нельзя опровергнуть. Надо сообщить заблуждающемуся уму такие знания, которые его просветят. Тогда заблуждения исчезнут сами собою.",
                                                       "Я тюлень.",
                                                       "Не надо о политике.",
                                                       "Я за царя.",
                                                       "У нас на Плутоне такого не допускается.",
                                                       "Не стоит доверять мне.",
                                                       "В тюрьме тоже за электричество платить не надо.",
                                                       "Минимализм есть беда соврееменного искусства.",
                                                       "Ну что я могу сказать... Это больно, но так оно и есть.",
                                                       "Один раз не п******.",
                                                       "Я сплю, отстаньте от меня.",
                                                       "Никого нет дома.",
                                                       "Давайте лучше поговорим о ювенальной юстиции.",
                                                       "Мудрецы очень любят детей. Во многом потому, что они не умеют пока еще так беззастенчиво лгать и легко притворяться, как то способны делать их родители",
                                                       "Как много необдуманных поступков надо совершить, чтобы понять, сколь многое зависит в нашей жизни от благоразумия!",
                                                       "Что может быть глупее самолюбования, особенно в преддверии каких-либо серьезных испытаний?",
                                                       "Есть люди, для которых вырывать друг у друга кусок изо рта занятие намного более приемлемое, чем потрудиться и вырастить собственный хлеб",
                                                       "Кто держится надменно с окружающими, тот сеет тернии на собственном пути. Продолжением наглых речей часто служат предсмертные хрипы",
                                                       "Трудно умереть за истину, но еще труднее жить для неё",
                                                       "Да ты охренел, братан!",
                                                       "У инопланетян такое не спрашивают.",
                                                       "Приезжай ко мне на Плутон и узнаешь ответ на свой вопрос.",
                                                       "Что ты сказал? Нука повтори? Я тебя щас по айпи вычислю.",
                                                       "Когда ты ешь, давай есть и собакам, даже если они тебя укусят",
                                                       "Самолюбие - это надутый воздухом шар; если его проколоть, из него вырываются бури",
                                                       "Глядя на тех, кто кривляется на политической сцене, хочется лишь плюнуть с отвращением и закрыть глаза, чтобы не видеть",
                                                       "Я не верю в коллективную мудрость невежественных индивидуумов",
                                                       "Лишь самые умные и самые глупые не могут измениться",
                                                       "Если у тебя не будет дурных мыслей, не будет и дурных поступков",
                                                       "Народ можно принудить к послушанию, но его нельзя принудить к знанию",
                                                       "Как мы можем знать, что такое смерть, когда мы не знаем еще,  что такое жизнь?",
                                                       "Всякое добро исходит от зла",
                                                       "Действовать - значит сражаться",
                                                       "Собственность - это кража",
                                                       "Лучше обнаруживать свой ум в молчании, чем в разговорах",
                                                       "Честь - это внешняя совесть, а совесть - это внутренняя честь",
                                                       "Проповедовать мораль легко, обосновать ее трудно",
                                                       "Годы учат многому, чего дни не знают",
                                                       "Давайте не будем касаться таких глубоких философских тем.",
                                                       "Ты шутишь? Это же риторический вопрос.",
                                                   };

        private static JohnState _state = JohnState.None;

        private static TelegramBotClient _bot 
            = new TelegramBotClient(System.IO.File.ReadAllText("Token.txt"));

        private static Timer _timer = new Timer();
        private static Timer _imageTimer = new Timer();
        private static Timer bashTimer = new Timer();

        private static Random _random = new Random();

        static void NewTimerInterval()
        {
            _timer.Interval = TimeSpan.FromMinutes(_random.Next(30, 61)).TotalMilliseconds;
            Console.WriteLine("TIMER New timer interval = " + _timer.Interval);
        }

        static void NewImageTimerInterval()
        {
            //_imageTimer.Interval = 10000;
            _imageTimer.Interval = TimeSpan.FromMinutes(_random.Next(30, 61)).TotalMilliseconds;
            Console.WriteLine("IMAGE TIMER New interval = " + _imageTimer.Interval);
        }

        static void NewBashInterval()
        {
            bashTimer.Interval = TimeSpan.FromMinutes(_random.Next(30, 61)).TotalMilliseconds;
            Console.WriteLine("BASH TIMER New interval = " + bashTimer.Interval);
        }

        static void Main(string[] args)
        {
            try
            {
                _timer.Elapsed += _timer_Elapsed;
                NewTimerInterval();
                _timer.Start();

                _imageTimer.Elapsed += _imageTimer_Elapsed;
                NewImageTimerInterval();
                _imageTimer.Start();

                bashTimer.Elapsed += BashTimer_Elapsed;
                NewBashInterval();
                bashTimer.Start();

                _bot.OnCallbackQuery += _bot_OnCallbackQuery;
                _bot.OnMessage += _bot_OnMessage;
                _bot.OnMessageEdited += _bot_OnMessageEdited;
                _bot.OnInlineQuery += _bot_OnInlineQuery;
                _bot.OnInlineResultChosen += _bot_OnInlineResultChosen;
                _bot.OnReceiveError += _bot_OnReceiveError;

                var me = _bot.GetMeAsync().Result;

                Console.Title = me.Username;

                _bot.StartReceiving();
                while (true)
                {
                    var text = Console.ReadLine();
                    if (text.StartsWith("1"))
                        _bot.SendTextMessageAsync(-169763388, text.Remove(0, 1));
                    else if (text.StartsWith("2"))
                        _bot.SendTextMessageAsync(-125354881, text.Remove(0, 1));
                }
                _bot.StopReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void BashTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            foreach (var chat in chatIds)
            {
                try
                {
                    _bot.SendTextMessageAsync(chat, bash());
                }
                catch
                {
                    _bot.SendTextMessageAsync(chat, "Извини братан, цитаты с баша не будет");
                }
            }

            NewBashInterval();
            _timer.Start();

            string bash()
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://bash.im/forweb/");
                string html = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();

                string[] numberDelims = { "'a href=\"http://bash.im/quote/", "\">#" };
                string number = html.Split(numberDelims, StringSplitOptions.None)[1];

                string[] quoteDelims = { "<' + 'div id=\"b_q_t\" style=\"padding: 1em 0;\">", "<' + '/div>" };
                string quote = html.Split(quoteDelims, StringSplitOptions.None)[1];

                quote = quote.Replace("<' + 'br />", Environment.NewLine);
                quote = quote.Replace("<' + 'br/>", Environment.NewLine);
                quote = quote.Replace("<' + 'br>", Environment.NewLine);
                quote = quote.Replace("&quot;", "\"");
                quote = quote.Replace("&lt;", "<");
                quote = quote.Replace("&gt;", ">");
                return $"Цитата с баша #{number}\n\n{quote}";
            }
        }

        private async static Task RandomImageCommand(Message message)
        {
            var pars = message.Text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (pars.Length <= 1)
            {
                try
                {
                    var imageToSend = new FileToSend();
                    using (var img = GoogleImage.GetGoogleRandomImage(out string selectedTopic))
                    {
                        imageToSend.Filename = "random.jpg";
                        imageToSend.Content = img;
                        var msg = await _bot.SendPhotoAsync(
                                      message.Chat.Id,
                                      imageToSend,
                                      $"Изображение на тему: {selectedTopic}");
                    }
                }
                catch (Exception e)
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Братан, гугл не отвечает");
                }
                return;
            }

            var image = new FileToSend();

            var searchstring = pars.Where((s, i) => i != 0).Aggregate("", (res, p) => res += " " + p);

            try
            {
                using (var img = GoogleImage.GetGoogleRandomImage(searchstring))
                {
                    image.Filename = "random.jpg";
                    image.Content = img;
                    var msg = await _bot.SendPhotoAsync(message.Chat.Id, image);
                }
            }
            catch (Exception e)
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, "Братан, гугл не отвечает");
            }
        }

        private static async void _imageTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _imageTimer.Stop();

            var image = new FileToSend();

            foreach (var chat in chatIds)
            {
                try
                {
                    using (var img = GoogleImage.GetGoogleRandomImage())
                    {
                        image.Filename = "random.jpg";
                        image.Content = img;
                        var msg = await _bot.SendPhotoAsync(chat, image);
                    }
                }
                catch
                {
                    await _bot.SendTextMessageAsync(chat, "Братан, гугл не отвечает");
                }
            }

            NewImageTimerInterval();
            _imageTimer.Start();
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            
            foreach (var chat in chatIds)
                _bot.SendTextMessageAsync(chat, _strings[_random.Next(0, _strings.Count)]);

            NewTimerInterval();
            _timer.Start();
        }

        private static void _bot_OnReceiveError(object sender, Telegram.Bot.Args.ReceiveErrorEventArgs e)
        {
            Console.WriteLine($"Error received. Message: {e.ApiRequestException.Message}");
        }

        private static void _bot_OnInlineResultChosen(object sender, Telegram.Bot.Args.ChosenInlineResultEventArgs e)
        {
            Console.WriteLine($"Inline result choosen. {e.ChosenInlineResult.ResultId}");
        }

        private static async void _bot_OnInlineQuery(object sender, Telegram.Bot.Args.InlineQueryEventArgs e)
        {
            Console.WriteLine($"InlineQuery. {e.InlineQuery.Query}");
        }

        private static void _bot_OnMessageEdited(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine($"MessageEdited. {e.Message.Text}");
        }

        private static async void _bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Console.WriteLine($"Message. {e.Message.Text}");            
            var message = e.Message;
            if (message.Chat.Type.Equals(ChatType.Group))
                Console.WriteLine($"Chat Id: {message.Chat.Id}");
            if (message == null || message.Type != MessageType.TextMessage) return;

            if (_state.Equals(JohnState.WaitingNameForFuck))
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, $"Пошёл нахрен, {message.Text}!",
                    replyMarkup: new ReplyKeyboardHide());
                _state = JohnState.None;
                return;
            }
            if (_state.Equals(JohnState.WaitingNameForHello))
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, $"Приветствую тебя, {message.Text}!",
                    replyMarkup: new ReplyKeyboardHide());
                _state = JohnState.None;
                return;
            }

            if (message.Text.StartsWith("Джон, "))
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, _johnAns[_random.Next(0, _johnAns.Count)]);
                return;
            }

            string command = message.Text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0];

            switch (command)
            {
                case "/help":
                case "/help@JohnFromPlutonBot":
                    HelpCommand(message); break;
                case "/sayhello":
                case "/sayhello@JohnFromPlutonBot":
                    SayHelloCommand(message); break;
                case "/sayfuck":
                case "/sayfuck@JohnFromPlutonBot":
                    SayFuckCommand(message); break;
                case "/sayopinion":
                case "/sayopinion@JohnFromPlutonBot":
                    SayOpinionCommand(message); break;
                case "/randomimage":
                case "/randomimage@JohnFromPlutonBot":
                    await RandomImageCommand(message); break;
            }
        }

        private static void _bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            Console.WriteLine($"CallbackQuery. {e.CallbackQuery.Message.Text}");
        }

        private static void HelpCommand(Message message)
        {
            string ans = "Здарова, братан! (или сестрица, хрен знает кто мне пишет)\n";
            ans += $"Вообщем так, нигер, я знаю что тебя зовут " +
                   $"{(string.IsNullOrEmpty(message.From.FirstName) ? "" : message.From.FirstName)}" +
                   $"{(string.IsNullOrEmpty(message.From.LastName) ? "" : $" {message.From.LastName}")}" +
                   $".\n\n";

            if (message.Chat.Type.Equals(ChatType.Group))
                ans +=
                    $"Собрались вы в этой группе под названием \"{message.Chat.Title}\" и спамите, засоряете только сигнал до Плутона.\n\n";

            ans += "Кароче, если хочешь передать привет, скажи /sayhello\n";
            ans += "Кароче, если хочешь послать кого-то, скажи /sayfuck\n";

            ans +=
                "Типа это, если хочешь что-то у меня просить, то начни вопрос с фразы 'Джон, ' и дальше свой вопрос\n";

            ans += "Давай, братан, до связи, не тупи только, умоляю.";

            _bot.SendTextMessageAsync(message.Chat.Id, ans);
        }

        private static void SayHelloCommand(Message message)
        {
            Say(message, "Приветствую тебя", false);
        }

        private static void SayFuckCommand(Message message)
        {
            Say(message, "Пошёл нахрен", true);
        }

        private static void SayOpinionCommand(Message message)
        {
            var pars = message.Text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            _bot.SendTextMessageAsync(message.Chat.Id, $"{_opinions[_random.Next(0, _opinions.Count)]}" +
                     $"{pars.Where((s, i) => i != 0).Aggregate("", (res, p) => res += " " + p)}!");
        }        

        private static void Say(Message message, string header, bool isBad)
        {
            var pars = message.Text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (pars.Length > 1)
            {
                _bot.SendTextMessageAsync(message.Chat.Id, $"{header}," +
                     $"{pars.Where((s, i) => i != 0).Aggregate("", (res, p) => res += " " + p)}!");
                return;
            }

            var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new [] // first row
                    {
                        new KeyboardButton("Дима"),
                        new KeyboardButton("Алёша"),
                        new KeyboardButton("Миша"),
                        new KeyboardButton("Саша"),
                    },
                    new [] // last row
                    {
                        new KeyboardButton("Гульназ"),
                        new KeyboardButton("Илья"),
                        new KeyboardButton("Айрат"),
                        isBad ? new KeyboardButton("Джо") : new KeyboardButton("Юра"),
                    }
                });

            _bot.SendTextMessageAsync(message.Chat.Id, $"Выберите жертву:", replyMarkup: keyboard);
            _state = isBad ? JohnState.WaitingNameForFuck : JohnState.WaitingNameForHello;
        }
    }
}
