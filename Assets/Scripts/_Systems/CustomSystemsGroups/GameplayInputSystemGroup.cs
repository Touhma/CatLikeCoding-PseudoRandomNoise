using Unity.Entities;

namespace _Systems.CustomSystemsGroups
{
    [UpdateInGroup(typeof(Unity.Scenes.SceneSystemGroup))]
    [UpdateAfter(typeof(GameInitializationSystemGroup))]
    public class GameplayInputSystemGroup : ComponentSystemGroup
    {
    }
}