﻿using System;
using System.ServiceModel.Channels;
using Microsoft.Xrm.Sdk;

namespace ThinkCrm.Core.PluginCore.Helper
{
    public static class CrmHelperExtensions
    {
        public static ExecutionMode GetMode(this IPluginExecutionContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            return (ExecutionMode) ctx.Mode;
        }

        public static PipelineStage GetPipelineStage(this IPluginExecutionContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            return (PipelineStage) ctx.Stage;
        }

        public static IsolationMode GetIsolationMode(this IPluginExecutionContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            return (IsolationMode) ctx.IsolationMode;
        }

        public static MessageType GetMessage(this IPluginExecutionContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            MessageType result;
            if (Enum.TryParse(ctx.MessageName, out result)) return result;
            return MessageType.UnknownReferToContextMessageString;

        }
    }

}
