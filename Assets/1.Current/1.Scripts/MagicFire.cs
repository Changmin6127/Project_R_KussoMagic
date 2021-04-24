using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MagicFire : MonoBehaviour  //Date Field
{
    public bool isActive { get; private set; } = false;
    private int hitCount = 0;
    private float addForce = 2000f;

    [SerializeField]
    private int endHitCount = 5;
    [SerializeField]
    private float deleteTime = 5;
    [SerializeField]
    private Rigidbody2D rig;
    [SerializeField]
    private Collider2D thisColloder;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioClip fireClip;
}

public partial class MagicFire : MonoBehaviour  //Function Field
{
    private void Start()
    {
        Deactive();
    }
    public void Active(float _charge)
    {
        StartCoroutine(DeleteTime());
        audioSource.PlayOneShot(fireClip);
        hitCount = 0;
        isActive = true;
        thisColloder.enabled = true;
        particle.Play(true);
        rig.velocity = Vector3.zero;

        rig.AddForce(transform.forward * (addForce * _charge));
    }
    public void Deactive()
    {
        StopAllCoroutines();
        isActive = false;
        rig.velocity = Vector3.zero;
        particle.Stop(true);
        thisColloder.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitCount++;
        if (hitCount >= endHitCount)
            Deactive();
        else
            audioSource.PlayOneShot(hitClip);
    }

    private IEnumerator DeleteTime()
    {
        yield return new WaitForSeconds(deleteTime);
        Deactive();
    }
}