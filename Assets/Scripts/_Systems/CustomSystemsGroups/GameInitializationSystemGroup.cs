using Unity.Entities;

namespace _Systems.CustomSystemsGroups
{
    [UpdateInGroup(typeof(Unity.Scenes.SceneSystemGroup))]
    public class GameInitializationSystemGroup: ComponentSystemGroup
    {
        
    }
}