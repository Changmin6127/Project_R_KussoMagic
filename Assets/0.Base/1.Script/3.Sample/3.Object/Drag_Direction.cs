namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class Drag_Direction : MonoBehaviour     //Data Field
    {
        enum Direction { None, Up, Down, Left, Right }

        private Direction direction = Direction.None;
        private Vector3 originPosition;
        private bool drag = false;
        private Vector3 screenSpace;
        private Vector3 offset;

        [SerializeField]
        private UnityEvent upDragEvent = null;
        [SerializeField]
        private UnityEvent downDragEvent = null;
        [SerializeField]
        private UnityEvent leftDragEvent = null;
        [SerializeField]
        private UnityEvent rightDragEvent = null;
    }

    public partial class Drag_Direction : MonoBehaviour     //Function Field
    {
        private void OnMouseDown()
        {
            originPosition = transform.position;
            screenSpace = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            drag = true;
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

            float x;
            float y;

            x = transform.position.x - originPosition.x;
            if (x < 0)
                x *= -1;
            y = transform.position.y - originPosition.y;
            if (y < 0)
                y *= -1;

            if (x > y)
                PositionX_Move();
            else
                PositionY_Move();

            switch (direction)
            {
                case Direction.Up: upDragEvent?.Invoke(); break;
                case Direction.Down: downDragEvent?.Invoke(); break;
                case Direction.Left: leftDragEvent?.Invoke(); break;
                case Direction.Right: rightDragEvent?.Invoke(); break;
            }

            transform.position = originPosition;
        }

        public void DragFinish()
        {
            drag = false;
        }

        private void PositionX_Move()
        {
            if (transform.position.x > originPosition.x) //오른쪽으로 드래그
            {
                direction = Direction.Right;
            }
            else if (transform.position.x < originPosition.x) //왼쪽로 드래그
            {
                direction = Direction.Left;
            }
        }

        private void PositionY_Move()
        {
            if (transform.position.y > originPosition.y) //위로 드래그
            {
                direction = Direction.Up;
            }
            else if (transform.position.y < originPosition.y) //아래로 드래그
            {
                direction = Direction.Down;
            }
        }
    }
}