namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class TextEffect : MonoBehaviour //Data Field
    {
        private float deltaTime = 0;
        private bool isActive = false;
        private float speed = 1f;
        private Vector3 originPosition;
        private Vector3 destinationPosition;
        private Color originColor;

        [SerializeField]
        private Text thisText = null;
    }

    public partial class TextEffect : MonoBehaviour //Function Field
    {
        public void Initialize(Color color, float speed)
        {
            thisText.color = color;
            this.speed = speed;
            originPosition = transform.localPosition;
            destinationPosition = originPosition + Vector3.up * 100;
            originColor = color;
        }

        public void Active(string value)
        {
            thisText.text = value;
            thisText.color = originColor;
            transform.localPosition = originPosition;
            deltaTime = 0;
            isActive = true;
            thisText.enabled = true;
        }

        public void Active(string value, Color color)
        {
            thisText.text = value;
            thisText.color = color;
            transform.localPosition = originPosition;
            deltaTime = 0;
            isActive = true;
            thisText.enabled = true;
        }

        private void Update()
        {
            if (isActive)
            {
                deltaTime += Time.deltaTime * speed;

                transform.localPosition = Vector3.Lerp(originPosition, destinationPosition, deltaTime);

                if (deltaTime > 1)
                {
                    isActive = false;
                    thisText.enabled = false;
                }
            }
        }

        public bool GetIsActive()
        {
            return isActive;
        }
    }
}