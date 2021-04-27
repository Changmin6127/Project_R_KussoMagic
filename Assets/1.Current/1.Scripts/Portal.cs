using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Portal : MonoBehaviour //Data Field
{
    private bool isAble = true;

    [SerializeField]
    private bool isStartDeactive = false;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform moveGuideTransform;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private PortalManager portalManager;
    [SerializeField]
    private UnityEvent activeEvent;
    [SerializeField]
    private UnityEvent regenEvent;
}

public partial class Portal : MonoBehaviour //Function Field
{
    private void Start()
    {
        portalManager.SignupPortal(this);

        if (isStartDeactive)
        {
            isAble = false;
            particle.Stop(true);
            activeEvent?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MagicFire magicFire = collision.GetComponent<MagicFire>();

        if (magicFire != null)
        {
            if (magicFire.isPlayerMagic)
            {
                isAble = false;
                portalManager.AllActive(this);
                particle.Stop(true);
                magicFire.Deactive();
                activeEvent?.Invoke();
                playerTransform.position = moveGuideTransform.position;
            }
        }
    }

    public void Deactive()
    {
        if (isAble)
            return;

        isAble = true;
        particle.Play(true);
        regenEvent?.Invoke();
    }
}