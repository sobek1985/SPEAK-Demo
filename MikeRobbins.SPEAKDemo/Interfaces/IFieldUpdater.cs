using Sitecore.Data.Items;

namespace MikeRobbins.SPEAKDemo.Interfaces
{
    public interface IFieldUpdater
    {
        void AddFieldsToItem<T>(Item item, T sourceObject) where T : Sitecore.Services.Core.Model.EntityIdentity;
    }
}