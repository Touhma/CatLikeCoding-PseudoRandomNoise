using Unity.Entities;

namespace _GlobalSharedSettings.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class SharedStaticsInitSystem : SystemBase
    {
        //private ExampleSettings _shaderGridSettings;
        //private const string ExampleSettingsPath = "ScriptableObject/Settings/ExampleSettings";        

        protected override void OnCreate()
        {
           
        }

        protected override void OnUpdate()
        {
            Enabled = false;
        }
        
        protected override void OnDestroy()
        {
           
        }
    }
}