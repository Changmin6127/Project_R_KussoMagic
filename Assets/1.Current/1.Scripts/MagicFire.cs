using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MagicFire : MonoBehaviour  //Date Field
{
    public bool isActive { get; private set; } = false;

    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private Collider2D thisColloder;
    [SerializeField]
    private ParticleSystem particle;
}

public partial class MagicFire : MonoBehaviour  //Function Field
{
    private void Start()
    {
        Deactive();
    }
    public void Active()
    {
        isActive = true;
        thisColloder.enabled = true;
        particle.Play(true);
        rig.velocity = Vector3.zero;
        rig.AddForce(transform.forward * 500);
    }
    public void Deactive()
    {
        isActive = false;
        rig.velocity = Vector3.zero;
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        thisColloder.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Deactive();
        }
    }
}