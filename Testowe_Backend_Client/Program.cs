
using System.Configuration;
using System.Text;
using Testowe_Backend_Client.Wrappers;
using Testowe_Backend_Client.VariousTimers;
using Testowe_Backend_Client.GitInfo;

//Timer for 15 seconds repeated calling of the excahnge with server
ITimer? timer= default(ITimer);

string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
string token = ConfigurationManager.ConnectionStrings["token"].ConnectionString;

if (args.Length >= 2)
{
    timer = new SimpleWrapper(args[0], args[1], connectionString)
        .GetTimer();
    //transmission.StartTransmiting();

    Console.WriteLine("Press anything to Exit after first exhange will be displayed.");
    timer.StartTimer();
    Console.ReadKey();
    return;
}

string WelcomeString = string.Empty;

try
{
    GitHubAPI gitHubAPI = new GitHubAPI(token);
    var message = gitHubAPI.GetGitHubRepositoryAsync("Magdod0", "Testowe_Backend").Result;
    WelcomeString = message.ToString();
}
catch (Exception ex)
{

}

if(string.IsNullOrEmpty(WelcomeString))
{
    WelcomeString = "Welcome to EncryptMessage App!";
}

var menuStringBuilder = new StringBuilder();
menuStringBuilder
    .AppendLine("\nSelect menu option:")
    .AppendLine("1) Write the new message and the key to Encrypt.")
    .AppendLine("2) Start checking for server reply.")
    .AppendLine("3) Stop checking for server reply.")
    .AppendLine("4) Clear all and show Menu")
    .AppendLine("Press Escape to exit the app.");

Console.WriteLine(WelcomeString);
Console.WriteLine(menuStringBuilder.ToString());

while (true)
{
    var reader = readLineWithCancel();
    // Exit on Escape
    if (reader.IsEscaped) return;

    switch (reader.Text)
    {
        case "1":
            Console.WriteLine("\nPlease write the message:");
            var messageReader = readLineWithCancel();

            Console.WriteLine("\nPlease write the key for the message encryption:");
            var keyReader = readLineWithCancel();

            if (keyReader.IsEscaped || messageReader.IsEscaped) break;
            Console.WriteLine("\nEncrypting the message");

            if (!string.IsNullOrEmpty(messageReader.Text) && !string.IsNullOrEmpty(keyReader.Text))
            {
                if (timer != null)
                    timer.StopTimer();
                timer = new SimpleWrapper(messageReader.Text, keyReader.Text, connectionString)
                    .GetTimer();
                Console.WriteLine("\nMessage received! Write 2 and Enter to launch the exchange!");
                break;
            }
            Console.WriteLine("\nThe message is empty, Press 1 to try again or press Escape to Exit!");

            Console.WriteLine(menuStringBuilder.ToString());
            break;

        case "2":
            if (timer == null)
            {
                Console.WriteLine("\nNo message has been added to database!");
                break;
            }
            // 
            Console.WriteLine("\nStarting Exchange!");
            timer.StartTimer();
            Console.WriteLine("\nWrite 3 and Enter to stop checking the server!");
            break;
        case "3":
            if (timer == null)
            {
                Console.WriteLine("\nNo message has been added to database!");
                break;
            }
            timer.StopTimer();
            Console.WriteLine("\nExchange stopped!");
            Console.WriteLine(menuStringBuilder.ToString());
            break;
        case "4":
            Console.Clear();
            Console.WriteLine(menuStringBuilder.ToString());
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
// CallBack Method for Timer
