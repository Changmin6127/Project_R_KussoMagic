namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class ObjectSoundCheck : MonoBehaviour   //Data Field
    {
        [SerializeField]
        private List<ObjectSound> objectList = new List<ObjectSound>();
    }

    public partial class ObjectSoundCheck : MonoBehaviour   //Function Field
    {
        private void Start()
        {
            Initialize();

        }

        private void Initialize()
        {
            for (int index = 0; index < objectList.Count; index++)
            {
                objectList[index].Initialize(this);
            }
        }
    }
}