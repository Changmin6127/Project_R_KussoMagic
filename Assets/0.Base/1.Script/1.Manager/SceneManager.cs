namespace Anvil
{
    using UnityEngine;

    public partial class SceneManager : MonoBehaviour //Data Field
    {
        public BaseScene CurrentScene { get; private set; }
    }
    public partial class SceneManager : MonoBehaviour //Initialize Function Field
    {
        public void Initialize()
        {

        }

        public void SignupCurrentScene(BaseScene currentScene)
        {
            CurrentScene = currentScene;
            CurrentScene.Initialize();
        }
    }
}