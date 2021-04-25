namespace Anvil
{
    using System.Collections.Generic;

    public partial class GameScene : BaseScene  //Data Field
    {
        public bool isNonPlayerControll { get; set; } = false;
        private Dictionary<string, int> poolingNames = new Dictionary<string, int>();
        private List<ObjectPooling> objectPoolings = new List<ObjectPooling>();
    }

    public partial class GameScene : BaseScene  //Main Function Field
    {
        private void Start()
        {
            MainSystem.Instance.SceneManager.SignupGameScene(this);
        }
    }

    public partial class GameScene : BaseScene  //Property Function Field
    {
        public override void Initialize()
        {
            base.Initialize();

            foreach (ObjectPooling Child in transform.GetComponentsInChildren<ObjectPooling>())
            {
                objectPoolings.Add(Child);
            }

            for (int index = 0; index < objectPoolings.Count; index++)
            {
                poolingNames.Add(objectPoolings[index].GetObjectName(), index);
            }
        }
    }

    public partial class GameScene : BaseScene  //Get Set Function Field
    {
        public ObjectPooling GetObjectPooling(string name)
        {
            return objectPoolings[poolingNames[name]];
        }
    }
}