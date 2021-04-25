namespace Anvil
{
    using UnityEngine;

    public partial class SceneManager : MonoBehaviour //Data Field
    {
        public LobbyScene LobbyScene { get; private set; }
        public GameScene GameScene { get; private set; }
    }
    public partial class SceneManager : MonoBehaviour //Initialize Function Field
    {
        public void Initialize()
        {

        }

        public void SignupLobbyScene(LobbyScene _lobbyScene)
        {
            LobbyScene = _lobbyScene;
            LobbyScene.Initialize();
        }

        public void SignupGameScene(GameScene _gameScene)
        {
            GameScene = _gameScene;
            GameScene.Initialize();
        }
    }
}