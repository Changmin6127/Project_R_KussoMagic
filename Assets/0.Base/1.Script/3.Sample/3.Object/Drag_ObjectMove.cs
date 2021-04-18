namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class Drag_ObjectMove : MonoBehaviour    //Data Field
    {
        private bool drag = false;
        private Vector3 screenSpace;
        private Vector3 offset;

        [SerializeField]
        private UnityEvent touchEnterEvent = null;
        [SerializeField]
        private UnityEvent touchExitEvent = null;
    }

    public partial class Drag_ObjectMove : MonoBehaviour    //Function Field
    {
        private void OnMouseDown()
        {
            screenSpace = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            drag = true;
            touchEnterEvent?.Invoke();
        }

        private void OnMouseDrag()
        {
            if (drag)
            {
                var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

                var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

                transform.position = curPosition;

            }

        }

        private void OnMouseUp()
        {
            drag = false;

            touchExitEvent?.Invoke();
        }

        public void DragFinish()
        {
            drag = false;
        }
    }
}