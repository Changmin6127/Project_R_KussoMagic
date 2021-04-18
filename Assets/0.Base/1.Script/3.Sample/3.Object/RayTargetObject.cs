namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class RayTargetObject : MonoBehaviour   //Data Field
    {
        [SerializeField]
        private UnityEvent activeEvent = null;
        [SerializeField]
        private UnityEvent finishEvent = null;
    }

    public partial class RayTargetObject : MonoBehaviour   //Override Function Field
    {
        public void Active()
        {
            activeEvent?.Invoke();
        }

        public void Finish()
        {
            finishEvent?.Invoke();
        }
    }
}