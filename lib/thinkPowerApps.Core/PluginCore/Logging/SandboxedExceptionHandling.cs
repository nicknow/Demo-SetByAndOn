using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ThinkCrm.Core.PluginCore.Logging
{
    public class SandboxedExceptionHandling
    {
        /// <summary>
        /// This utility method can be used for retrieving details of exeception objects when Reflection is prohibited by Sandbox.
        /// </summary>
        /// <param name="_exception">Exception.</param>        
        public static string GetExtendedExceptionDetails(object _exception, string indent = null)
        {            

            try
            {
                if (_exception == null) throw new ArgumentNullException(nameof(_exception));

                var sb = new StringBuilder(indent);
                sb.AppendLine($"{indent}SandboxedExceptionHandling");
                sb.AppendLine($"{indent}Type: {_exception.GetType().FullName}");                

                if (_exception is AggregateException)
                {
                    var ex = (AggregateException) _exception;
                    sb.Append($"{indent}AggregateException.").AppendLine();
                    sb.Append($"{indent}Type: {ex.GetType().Name}").AppendLine();
                    sb.Append($"{indent}Message: {ex.Message}").AppendLine();
                    sb.Append($"{indent}Stack Trace: {ex.StackTrace}").AppendLine();
                    sb.Append($"{indent}Aggregate Exceptions ({ex.InnerExceptions?.Count}").AppendLine();
                    if (ex.InnerExceptions != null && ex.InnerExceptions.Any()) ex.InnerExceptions.ToList().ForEach(x => GetExtendedExceptionDetails(x, "   " + indent));
                }
                else if (_exception is FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
                {
                    var ex = (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>) _exception;
                    sb.Append($"{indent}FaultException").AppendLine();
                    sb.Append($"{indent}Timestamp: {ex.Detail.Timestamp}").AppendLine();
                    sb.Append($"{indent}Code: {ex.Detail.ErrorCode}").AppendLine();
                    sb.Append($"{indent}Message: {ex.Detail.Message}").AppendLine();
                    sb.Append($"{indent}Inner Fault: {(ex.Detail.InnerFault != null ? ex.Detail.InnerFault.Message : "(No Inner Exception)")}").AppendLine();
                    if (ex.Detail.InnerFault != null)
                        sb.Append(GetExtendedExceptionDetails(ex.Detail.InnerFault, "   " + indent)).AppendLine();

                }
                else if (_exception is TimeoutException)
                {
                    var ex = (TimeoutException) _exception;
                    sb.Append($"{indent}Timeout Exception").AppendLine();
                    sb.Append($"{indent}Message: {ex.Message}").AppendLine();
                    sb.Append($"{indent}Stack Trace: {ex.StackTrace}").AppendLine();
                    sb.Append($"{indent}Inner Fault: {(ex.InnerException?.Message ?? "(No Inner Exception)")}").AppendLine();
                    if (ex.InnerException != null)
                        sb.Append(GetExtendedExceptionDetails(ex.InnerException, "  " + indent)).AppendLine();
                }
                else if (_exception is KeyNotFoundException)
                {
                    var ex = (KeyNotFoundException) _exception;
                    sb.Append($"{indent}KeyNotFoundException Exception").AppendLine();
                    sb.Append($"{indent}Message: {ex.Message}").AppendLine();
                    sb.Append($"{indent}Stack Trace: {ex.StackTrace}").AppendLine();
                    sb.Append($"{indent}Inner Fault: {(ex.InnerException?.Message ?? "(No Inner Exception)")}").AppendLine();
                    if (ex.InnerException != null)
                        sb.Append(GetExtendedExceptionDetails(ex.InnerException, "  " + indent)).AppendLine();

                }
                else if (_exception is Exception)
                {
                    var ex = (Exception) _exception;
                    sb.Append($"{indent}Exception.").AppendLine();
                    sb.Append($"{indent}Type: {ex.GetType().Name}").AppendLine();
                    sb.Append($"{indent}Message: {ex.Message}").AppendLine();
                    sb.Append($"{indent}Stack Trace: {ex.StackTrace}").AppendLine();
                    sb.Append($"{indent}Inner Fault: {(ex.InnerException?.Message ?? "(No Inner Exception)")}").AppendLine();
                    if (ex.InnerException != null)
                        sb.Append(GetExtendedExceptionDetails(ex.InnerException, "  " + indent)).AppendLine();
                }
                else
                {
                    sb.Append(
                        $"SandboxExceptionHandling.GetExtendedExceptionDetails: Object type is not an Exception ({_exception.GetType()} / {_exception})");
                }

                return sb.ToString();
            }
            catch (Exception exception)
            {
                //log or swallow here
                return
                    $"GetExtendedExceptionDetails: Exception during logging of Exception message: {exception.Message}";
            }
        }
    }
}

