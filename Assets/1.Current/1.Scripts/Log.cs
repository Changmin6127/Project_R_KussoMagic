using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Log : MonoBehaviour    //Data Field
{
    private bool isTrigger = false;

    [SerializeField]
    private Rigidbody2D trigger;
    [SerializeField]
    private Rigidbody2D mainRigid;
    [SerializeField]
    private UnityEvent hitEvent;
}

public partial class Log : MonoBehaviour    //Function Field
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MagicFire magicFire = collision.collider.GetComponent<MagicFire>();

        if (magicFire != null && isTrigger == false)
        {
            isTrigger = true;
            mainRigid.constraints = RigidbodyConstraints2D.None;
            trigger.gravityScale = 1;
            magicFire.Deactive();
            hitEvent?.Invoke();
        }
    }
}