
using System.Configuration;
using System.Text;
using Testowe_Backend_Client.Wrappers;
using Testowe_Backend_Client.GitInfo;
using Testowe_Backend_Client.Common.Timers;
using System.Drawing.Text;
using Testowe_Backend_Client.Common;
using Testowe_Backend_Client.Common.ExchangeBuilders;
using Microsoft.Identity.Client;

var settings = GetInfo();
var builder = GetBuilder(settings);
var timer = GetTimer(builder);


while (true)
{
    var reader = readLineWithCancel();
    // Exit on Escape
    if (reader.IsEscaped) return;

    switch (reader.Text)
    {
        case "1":
            // 
            Console.WriteLine("\nStarting Exchange!");
            timer.StartTimer();
            Console.WriteLine("\nWrite 2 and Enter to stop checking the server!");
            break;
        case "2":
            timer.StopTimer();
            Console.WriteLine("\nExchange stopped!");

            break;

        default: Console.WriteLine("\nThat's not a console option, please select from list! Press Escape if you want to Exit!"); break;
    }
}


//Like ReadLine but with Escape option
static (string Text, bool IsEscaped) readLineWithCancel()
{
    StringBuilder stringBuilder = new StringBuilder();
    ConsoleKeyInfo Info = Console.ReadKey(true);

    while (Info.Key != ConsoleKey.Enter && Info.Key != ConsoleKey.Escape)
    {
        stringBuilder.Append(Info.KeyChar);
        Console.Write(Info.KeyChar);
        Info = Console.ReadKey(true);
    }

    if (Info.Key == ConsoleKey.Enter) return (stringBuilder.ToString(), false);

    return (stringBuilder.ToString(), true);
}


UserSettings GetInfo()
{
    var message = QuestionManager.Read("Please write new message to Encrypt and key!\nMessage:\n");
    var key = QuestionManager.Read("Key:\n");
    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    var host = ConfigurationManager.AppSettings["HostAddress"].ToString();
    return new UserSettings(host, message, key, connectionString);
}
IExchangeBuilder GetBuilder(UserSettings settings)
{
    var builders = new IExchangeBuilder[]
    {
        new MessageExchangeBuilder(settings)
    };
    return QuestionManager.Choose(builders, e => e.GetType().Name, "Choose builder:");
}
ITimer GetTimer(IExchangeBuilder builder)
{
    var builders = new ITimer[]
    {
        new SimpleTimer(builder)
    };
    return QuestionManager.Choose(builders, e => e.GetType().Name, "Choose timer:");
}



















































////Timer for 15 seconds repeated calling of the excahnge with server
//ITimer? timer= default(ITimer);

//string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//string token = ConfigurationManager.AppSettings["tokenRead"].ToString(); 

//if (args.Length >= 2)
//{
//    timer = new SimpleWrapper(args[0], args[1], connectionString)
//        .GetTimer();
//    //transmission.StartTransmiting();

//    Console.WriteLine("Press anything to Exit after first exhange will be displayed.");
//    timer.StartTimer();
//    Console.ReadKey();
//    return;
//}

//string WelcomeString = string.Empty;

//try
//{
//    GitHubAPI gitHubAPI = new GitHubAPI(token);
//    var message = gitHubAPI.GetGitHubRepositoryAsync("Magdod0", "Testowe_Winforms_Devexpress").Result;
//    WelcomeString = message.ToString();
//}
//catch (Exception ex)
//{

//}

//if(string.IsNullOrEmpty(WelcomeString))
//{
//    WelcomeString = "Welcome to EncryptMessage App!";
//}

//var menuStringBuilder = new StringBuilder();
//menuStringBuilder
//    .AppendLine("\nSelect menu option:")
//    .AppendLine("1) Write the new message and the key to Encrypt.")
//    .AppendLine("2) Start checking for server reply.")
//    .AppendLine("3) Stop checking for server reply.")
//    .AppendLine("4) Clear all and show Menu")
//    .AppendLine("Press Escape to exit the app.");

//Console.WriteLine(WelcomeString);
//Console.WriteLine(menuStringBuilder.ToString());


//// CallBack Method for Timer
