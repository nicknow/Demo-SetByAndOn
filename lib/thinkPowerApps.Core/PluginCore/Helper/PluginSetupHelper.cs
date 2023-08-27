using System;
using Microsoft.Xrm.Sdk;
using ThinkCrm.Core.Interfaces;
using ThinkCrm.Core.PluginCore.Attributes;

namespace ThinkCrm.Core.PluginCore.Helper
{
    public class PluginSetupHelper : IPluginSetupHelper
    {
        private readonly IPluginSetup _pluginSetup;
        private readonly ILogging _logging;
        private readonly string _className;

        internal PluginSetupHelper(IPluginSetup pluginSetup)
        {
            _pluginSetup = pluginSetup;
            _logging = _pluginSetup.Logging;
            _className = GetType().FullName;
        }

        public Entity GetTargetEntity()
        {
            if (_pluginSetup.Context.InputParameters.Contains(PluginConstants.Target) &&
                _pluginSetup.Context.InputParameters[PluginConstants.Target] is Entity)
            {
                return (Entity)_pluginSetup.Context.InputParameters[PluginConstants.Target];
            }
            _logging.WithCaller(_className).Write(
                "Error: InputParameters does not contain a Target or Target is not Entity. Contains: {0} / Type: {1}",
                _pluginSetup.Context.InputParameters.Contains(PluginConstants.Target),
                _pluginSetup.Context.InputParameters.Contains(PluginConstants.Target)
                    ? _pluginSetup.Context.InputParameters[PluginConstants.Target].GetType().ToString()
                    : "(Not Applicable)");
            throw new InvalidPluginExecutionException(PluginConstants.UserErrorMessage);
        }

        public T GetTargetEntity<T>() where T: Entity
        {
            try
            {
                return this.GetTargetEntity().ToEntity<T>();
            }
            catch (InvalidPluginExecutionException ex)
            {
                _logging.WithCaller(_className).Write(ex);
                throw;
            }
            catch (Exception ex)
            {
                _logging.WithCaller(_className).Write(ex);
                throw new InvalidPluginExecutionException(PluginConstants.UserErrorMessage, ex);
            }
        }

        public EntityReference GetTargetReference()
        {
            if (_pluginSetup.Context.InputParameters.Contains(PluginConstants.Target) &&
                _pluginSetup.Context.InputParameters[PluginConstants.Target] is EntityReference)
            {
                return (EntityReference)_pluginSetup.Context.InputParameters[PluginConstants.Target];
            }
            _logging.WithCaller(_className).Write(
                "Error: InputParameters does not contain a Target or Target is not EntityReference. Contains: {0} / Type: {1}",
                _pluginSetup.Context.InputParameters.Contains(PluginConstants.Target),
                _pluginSetup.Context.InputParameters.Contains(PluginConstants.Target)
                    ? _pluginSetup.Context.InputParameters[PluginConstants.Target].GetType().ToString()
                    : "(Not Applicable)");
            throw new InvalidPluginExecutionException(PluginConstants.UserErrorMessage);
        }

        public Entity GetImage(string imageName, ImageType imageType, bool skipOnCreate)
        {
            if (skipOnCreate && _pluginSetup.Context.GetMessage() == MessageType.Create) return _logging.WithCaller(_className).WriteAndReturn<Entity>(null,"Returning null for GetImage due to Create message and skipOnCreate set true.");

            var imageCollection = imageType == ImageType.PreImage ? _pluginSetup.Context.PreEntityImages : _pluginSetup.Context.PostEntityImages;

            if (imageCollection.Contains(imageName))
                return _logging.WithCaller(_className).WriteAndReturn((Entity) imageCollection[imageName],
                    $"Found {imageType.ToString()} named {imageName}");
            
            _logging.WithCaller(_className).Write($"Error: {(imageType == ImageType.PreImage ? "PreEntityImages" : "PostEntityImages")} does not contain {imageName}");
            
            throw new InvalidPluginExecutionException(PluginConstants.UserErrorMessage);
        }

        public Entity GetImage<T>(string imageName, ImageType imageType, bool skipOnCreate) where T : Entity
        {
            var image = GetImage(imageName, imageType, skipOnCreate);
            if (image == null) return null;
            return image.ToEntity<T>();
        }

    }
}
