using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using ThinkCrm.Core.Interfaces;
using ThinkCrm.Core.PluginCore;
using ThinkCrm.Core.PluginCore.Attributes;
using ThinkCrm.Core.PluginCore.Logging;
using YamlConverter;
using YamlDotNet.Core;

namespace ForceOnAndByFields
{
    [TargetEntity()]
    [Pipeline(ThinkCrm.Core.PluginCore.Helper.PipelineStage.Preoperation, false)]
    [Pipeline(ThinkCrm.Core.PluginCore.Helper.MessageType.Create, false)]    
    public class SetByAndOn : CorePlugin
    {
        public SetByAndOn() { }

        
        protected override void Execute(IPluginSetup p)
        {

            var logger = new LocalLogger(p.Logging, ClassName);

            logger.Write("Experimental Plugin Logic. Not intended for production use in current as-is state.");

            var target = p.Helper.GetTargetEntity();

            var tagText = FindInParentContextTree(p.Context, nameof(p.Context.InputParameters), "tag")?.ToString();

            if (string.IsNullOrEmpty(tagText))
            {
                logger.Write("tagText is null or empty. Exiting plugin.");
                return;
            }

            logger.Write($"tagText: \n [START] \n {tagText} \n [END]");

            var replace = tagText.Substring(0, 1) == "{" ? ProcessJson<OnAndByValues>(tagText, logger) : ProcessYaml<OnAndByValues>(tagText, logger);

            if (replace == null)
            {
                logger.Write("tag deserialization resulted in null object. Exiting plugin.");
                return;
            }

            if (!string.IsNullOrEmpty(replace.By))
            {
                logger.Write($"By: {replace.By}");
                Guid userId;
                if (Guid.TryParse(replace.By, out userId))
                {
                    logger.Write($"Setting createdby and modifiedby to {userId}");
                    target["createdby"] = new EntityReference("systemuser", userId);
                    target["modifiedby"] = new EntityReference("systemuser", userId);
                }
                else
                {
                    logger.Write("Could not successfully parse By to a Guid. Skipping.");
                }
            }

            if(replace.On != null)
            {
                logger.Write($"Setting createdon and modifiedon to {replace.On}");
                target["createdon"] = replace.On;
                target["modifiedon"] =  replace.On;

                    
            }
            else
            {
                logger.Write("On is null. Skipping.");
            }


            logger.Write($"{ClassName} completed operation successfully.");



        }

        private object FindInParentContextTree(IPluginExecutionContext context, string parameterCollectionName, string key)
        {
            var type = context.GetType();
            var propInfo = type.GetProperty(parameterCollectionName);
            if (propInfo == null) return string.Empty;
            var parameterCollection = propInfo.GetValue(context) as ParameterCollection;
            if (parameterCollection == null) return string.Empty;

            if (parameterCollection.Contains(key)) return parameterCollection[key];

            if (context.ParentContext != null) return FindInParentContextTree(context.ParentContext, parameterCollectionName, key);

            return null;

        }

        private T ProcessYaml<T>(string incomingText, ILogging logger)
        {
            logger.Write("Yaml Deserialization");
            try
            {
                var output = YamlConvert.DeserializeObject<T>(incomingText);
                logger.Write("Yaml Deserialization completed without error.");
                return output;
            }
            catch (YamlException ex)
            {
                logger.Write("YamlException Deserializing Yaml");
                logger.Write($"incomingText: \n [START] \n {incomingText} \n [END]");
                logger.Write(ex);
                return default(T);
            }
            catch(Exception ex)
            {
                logger.Write("Unknown Exception Deserializing Yaml");
                logger.Write($"incomingText: \n [START] \n {incomingText} \n [END]");
                logger.Write(ex);
                return default(T);
            }            
        }

        private T ProcessJson<T>(string incomingText, ILogging logger)
        {
            logger.Write("Json Deserialization");
            try
            {
                var output = JsonConvert.DeserializeObject<T>(incomingText);
                logger.Write("Json Deserialization completed without error.");
                return output;
            }
            catch (JsonException ex)
            {
                logger.Write("JsonException Deserializing Json");
                logger.Write($"incomingText: \n [START] \n {incomingText} \n [END]");
                logger.Write(ex);
                return default(T);
            }
            catch (Exception ex)
            {
                logger.Write("Unknown Exception Deserializing Json");
                logger.Write($"incomingText: \n [START] \n {incomingText} \n [END]");
                logger.Write(ex);
                return default(T);
            }
        }        

    }


}
