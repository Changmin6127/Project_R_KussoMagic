namespace Anvil
{
    using System.Collections.Generic;

    public partial class LobbyScene : BaseScene  //Data Field
    {
        private Dictionary<string, int> poolingNames = new Dictionary<string, int>();
        private List<ObjectPooling> objectPoolings = new List<ObjectPooling>();
    }

    public partial class LobbyScene : BaseScene  //Main Function Field
    {
        private void Update()
        {

        }
    }

    public partial class LobbyScene : BaseScene  //Property Function Field
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

    public partial class LobbyScene : BaseScene  //Get Set Function Field
    {
        public ObjectPooling GetObjectPooling(string name)
        {
            return objectPoolings[poolingNames[name]];
        }
    }
}