using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AspectCoreSample
{
    public class CustomInterceptor:AbstractInterceptor
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var logger = context.ServiceProvider.GetService<ILogger<CustomInterceptor>>();
            try
            {
                var paras = "";
                context.Parameters.ToList().ForEach(o=>paras+=o.ToString()+",");
                paras.Substring(0,paras.Length-1);
                logger.LogInformation($"Before Action. params is '{ paras }'");
                await next(context);
                logger.LogInformation("After Action.");
            }
            catch (Exception e)
            {
                logger?.LogWarning("Catch Exception");
                throw;
            }
            finally
            {
                Console.WriteLine("After service call");
            }
        }
    }
}