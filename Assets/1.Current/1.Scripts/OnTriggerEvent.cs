using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Anvil;

public partial class OnTriggerEvent : MonoBehaviour //Data Field
{
    private bool onTriggerEnterCooltime = false;
    private bool onTriggerExitCooltime = false;
    private bool onCollisionEnterCooltime = false;
    private bool onCollisionExitCooltime = false;

    [SerializeField]
    private UnityEvent onTriggerEnterEvent;
    [SerializeField]
    private UnityEvent onTriggerExitEvent;
    [SerializeField]
    private UnityEvent onCollisionEnterEvent;
    [SerializeField]
    private UnityEvent onCollisionExitEvent;
}

public partial class OnTriggerEvent : MonoBehaviour //Function Field
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTriggerEnterCooltime)
            return;

        onTriggerEnterCooltime = true;
        MainSystem.Instance.CoroutineManager.WaitForSecond_Action(OnTriggerEnterCooltime, 0.1f);
        onTriggerEnterEvent?.Invoke();
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (onTriggerExitCooltime)
            return;

        onTriggerExitCooltime = true;
        MainSystem.Instance.CoroutineManager.WaitForSecond_Action(OnTriggerExitCooltime, 0.1f);
        onTriggerExitEvent?.Invoke();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCollisionEnterCooltime)
            return;

        onCollisionEnterCooltime = true;
        MainSystem.Instance.CoroutineManager.WaitForSecond_Action(OnCollisionEnterCooltime, 0.1f);
        onCollisionEnterEvent?.Invoke();
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (onCollisionExitCooltime)
            return;

        onCollisionExitCooltime = true;
        MainSystem.Instance.CoroutineManager.WaitForSecond_Action(OnCollisionExitCooltime, 0.1f);
        onCollisionExitEvent?.Invoke();
    }

    private void OnTriggerEnterCooltime()
    {
        onTriggerEnterCooltime = false;
    }

    private void OnTriggerExitCooltime()
    {
        onTriggerExitCooltime = false;
    }

    private void OnCollisionEnterCooltime()
    {
        onCollisionEnterCooltime = false;
    }

    private void OnCollisionExitCooltime()
    {
        onCollisionExitCooltime = false;
    }
}