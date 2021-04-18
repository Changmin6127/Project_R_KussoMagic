namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class RaycastObject : MonoBehaviour    //Data Field
    {
        private RaycastHit hit;
        private bool isGameOver = false;

        [SerializeField]
        private Transform lookAtTarget = null;
        [SerializeField]
        private UnityEvent gameoverEvent = null;
    }

    public partial class RaycastObject : MonoBehaviour    //Function Field
    {
        private void Update()
        {
            if (isGameOver == false)
            {
                transform.LookAt(lookAtTarget);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 6, Color.red);

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 6))
                {
                    if (hit.transform.gameObject.layer == 9)
                    {
                        isGameOver = true;
                        gameoverEvent?.Invoke();
                    }
                }
            }
        }
    }
}