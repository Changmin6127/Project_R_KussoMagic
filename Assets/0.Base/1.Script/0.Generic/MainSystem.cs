namespace Anvil
{
    public partial class MainSystem : GenericSingleton<MainSystem> //Managers Field
    {
        public UserDataManager UserDataManager { get; private set; }
        public SceneManager SceneManager { get; private set; }
        public CoroutineManager CoroutineManager { get; private set; }
        public RankingManager RankingManager { get; private set; }
        public SoundManager SoundManager { get; private set; }
    }
    public partial class MainSystem : GenericSingleton<MainSystem> //Initialize Function Field
    {
        public void Initialize()
        {
            Allocate();
        }
        private void Allocate()
        {
            UserDataManager = gameObject.AddComponent<UserDataManager>();
            SceneManager = gameObject.AddComponent<SceneManager>();
            CoroutineManager = gameObject.AddComponent<CoroutineManager>();
            RankingManager = gameObject.AddComponent<RankingManager>();
            SoundManager = gameObject.AddComponent<SoundManager>();
        }
    }
    public partial class MainSystem : GenericSingleton<MainSystem> //Property Function Field
    {
        public void SystemStart()
        {
            Initialize();
        }

    }
}