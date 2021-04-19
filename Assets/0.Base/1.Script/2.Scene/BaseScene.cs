namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using System;

    public partial class BaseScene : MonoBehaviour  //Data Field
    {
        [SerializeField]
        private UnityEvent sceneStartEvent;
    }

    public partial class BaseScene : MonoBehaviour  //Function Field
    {
        public void Start()
        {
            MainSystem.Instance.SceneManager.SignupCurrentScene(this);
        }
        public virtual void Initialize()
        {
            sceneStartEvent?.Invoke();
        }
    }

}