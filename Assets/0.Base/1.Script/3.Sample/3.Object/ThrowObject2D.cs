namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class ThrowObject2D : MonoBehaviour   //Data Field
    {
        private float deltaTime = 0;
        private bool isActive = false;
        private string userID = string.Empty;

        [SerializeField]
        private Rigidbody2D thisRigidbody = null;
        [SerializeField]
        private float speed = 1500;
        [SerializeField]
        private float time = 1;
        [SerializeField]
        private UnityEvent activeEvent = null;
    }

    public partial class ThrowObject2D : MonoBehaviour   //Function Field
    {
        private void OnEnable()
        {
            deltaTime = 0;
            isActive = true;
            thisRigidbody.AddForce(transform.up * speed);
            activeEvent?.Invoke();
        }

        private void OnDisable()
        {
            isActive = false;
            thisRigidbody.velocity = Vector3.zero;
        }

        private void Update()
        {
            if (isActive)
            {
                deltaTime += Time.deltaTime;
                if (deltaTime > time)
                {
                    isActive = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public partial class ThrowObject2D : MonoBehaviour   //Get Set Function Field
    {
        public void SetUserID(string value)
        {
            userID = value;
        }

        public string GetUserID()
        {
            return userID;
        }
    }
}