namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class ObjectSound : MonoBehaviour    //Data Field
    {
        private ObjectSoundCheck objectSoundCheck;
    }

    public partial class ObjectSound : MonoBehaviour    //Function Field
    {
        public void Initialize(ObjectSoundCheck value)
        {
            objectSoundCheck = value;
        }
    }
}