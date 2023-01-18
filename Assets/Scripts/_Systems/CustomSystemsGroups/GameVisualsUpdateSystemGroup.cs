using Unity.Entities;

namespace _Systems.CustomSystemsGroups
{
    [UpdateInGroup(typeof(Unity.Scenes.SceneSystemGroup))]
    [UpdateAfter(typeof(GameplayInputSystemGroup))]
    public class GameVisualsUpdateSystemGroup: ComponentSystemGroup
    {
        
    }
}