using Microsoft.Xrm.Sdk;
using ThinkCrm.Core.PluginCore.Attributes;

namespace ThinkCrm.Core.Interfaces
{
    public interface IPluginSetupHelper
    {
        Entity GetTargetEntity();
        T GetTargetEntity<T>() where T: Entity;
        EntityReference GetTargetReference();

        Entity GetImage(string imageName, ImageType imageType, bool skipOnCreate);

        Entity GetImage<T>(string imageName, ImageType imageType, bool skipOnCreate) where T : Entity;
    }
}