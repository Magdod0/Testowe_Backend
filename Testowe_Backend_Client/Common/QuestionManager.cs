using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testowe_Backend_Client.Common
{
    public static class QuestionManager
    {
        /// <summary>
        /// Считать ответ пользователя на вопрос.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static string Read(string question)
        {
            string buffer = string.Empty;
            do
            {
                SysConsole.WriteQuestionLine(question);
                buffer = (Console.ReadLine() ?? string.Empty).Trim();
            } while (string.IsNullOrEmpty(buffer));
            return buffer;
        }
        /// <summary>
        /// Выбор одного из вариантогв в списке.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="results"></param>
        /// <param name="getName"></param>
        /// <param name="header"></param>
        /// <param name="footer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static TResult Choose<TResult>(
            IEnumerable<TResult> results,
            Func<TResult, string> getName,
            string header = "",
            string footer = "")
        {
            var arrayResult = results.ToArray();
            if(!arrayResult.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(results));
            }

            if (arrayResult.Length == 1)
                return arrayResult[0];

            int selectedIndex = -1;
            do
            {
                if (!string.IsNullOrEmpty(header))
                    SysConsole.WriteQuestionLine(header);

                foreach (var i in arrayResult.Length)
                    SysConsole.WriteLine($"{i + 1} {getName(arrayResult[i])}");

                if (!string.IsNullOrEmpty(footer))
                    SysConsole.WriteQuestionLine(footer);

                SysConsole.Write($"Select Number between 1 and {arrayResult.Length}:");
                if(arrayResult.Length < 10)
                {
                    selectedIndex = (int)char.GetNumericValue(Console.ReadKey().KeyChar) - 1;
                    Console.WriteLine();
                }
                else
                {
                    var input = Console.ReadLine();
                    int.TryParse(input, out selectedIndex);
                    selectedIndex -= 1;
                }


            } while (selectedIndex < 0 || selectedIndex >= arrayResult.Length);

            SysConsole.Write("Selected: ");
            SysConsole.WriteLine(getName(arrayResult[selectedIndex]));
            Console.WriteLine();

            return arrayResult[selectedIndex];
        }
    }
}
