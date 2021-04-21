using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MagicFire : MonoBehaviour  //Date Field
{
    [SerializeField]
    private Rigidbody rig;
    [SerializeField]
    private Collider thisColloder;
    [SerializeField]
    private ParticleSystem particle;
}

public partial class MagicFire : MonoBehaviour  //Function Field
{
    private void Start()
    {
        rig.velocity = Vector3.zero;
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        thisColloder.enabled = false;
    }
    public void Active()
    {
        thisColloder.enabled = true;
        particle.Play(true);
        rig.velocity = Vector3.zero;
        rig.AddForce(transform.forward * 300);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rig.velocity = Vector3.zero;
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            thisColloder.enabled = false;
        }
    }
}