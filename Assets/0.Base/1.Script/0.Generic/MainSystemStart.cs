namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class MainSystemStart : MonoBehaviour    //Data Field
    {
        [SerializeField]
        private UnityEvent finishEvent = null;
    }

    public partial class MainSystemStart : MonoBehaviour    //Function Field
    {
        private void Awake()
        {
            MainSystem.Instance.SystemStart();
            finishEvent?.Invoke();
        }
    }
}