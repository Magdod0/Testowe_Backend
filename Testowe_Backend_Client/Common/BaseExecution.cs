using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testowe_Backend_Client.Common
{
    public abstract class BaseExecution
    {
        ///// <summary>
        ///// Название выбранного.
        ///// </summary>
        //public abstract string Name { get; }
        ///// <summary>
        ///// Выполнить действие.
        ///// </summary>
        ///// <param name="settings"></param>
        //public abstract void Execute();
        protected bool TrySafeCall<TResult>(Func<TResult> action, out TResult result)
        {
            result = default(TResult);
            try
            {
                result = action();
                return true;
            }catch (Exception ex)
            {
                PrintError(ex);
                return false;
            }
        }
        protected bool TrySafeCall<TResult, TEntity>(Func<TResult, TEntity> action, TResult sample, out TEntity result)
        {
            result = default(TEntity);
            try
            {
                result = action(sample);
                return true;
            }
            catch (Exception ex)
            {
                PrintError(ex);
                return false;
            }
        }
        protected bool TrySafeCall<TResult>(Func<TResult> action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                PrintError(ex);
                return false;
            }
        }
        protected bool TrySafeCall(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                PrintError(ex);
                return false;
            }
        }
        void PrintError(Exception ex)
        {
            var errorType = ex.GetType();
            var type = errorType.IsGenericType
                ? $"{errorType.Name}<{string.Join(", ", errorType.GetGenericArguments().Select(arg => arg.Name))}"
                : errorType.Name;
            SysConsole.WriteLine($"[{type}]:" + ex.Message);
        }

    }
}
