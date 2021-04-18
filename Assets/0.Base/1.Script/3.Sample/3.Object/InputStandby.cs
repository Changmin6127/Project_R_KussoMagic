namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class InputStandby : MonoBehaviour   //Data Field
    {
        [SerializeField]
        private KeyCode keyCode = KeyCode.None;
        [SerializeField]
        private UnityEvent activeEvent = null;
    }

    public partial class InputStandby : MonoBehaviour   //Function Field
    {
        private void Update()
        {
            if (Input.GetKeyDown(keyCode) && transform.gameObject.activeSelf)
                activeEvent?.Invoke();
        }
    }
}