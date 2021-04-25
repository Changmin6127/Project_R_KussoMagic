using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil;

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
    [SerializeField]
    private List<ParticleSystem> fireEffects = new List<ParticleSystem>();
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
        foreach(ParticleSystem particle in fireEffects)
        {
            particle.Play(true);
        }
        rig.velocity = Vector3.zero;

        rig.AddForce(transform.forward * (addForce * _charge));
    }
    public void Deactive()
    {
        StopAllCoroutines();
        isActive = false;
        rig.velocity = Vector3.zero;
        foreach (ParticleSystem particle in fireEffects)
        {
            particle.Stop(true);
        }
        thisColloder.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();

        if(_player != null)
        {
            _player.rig.constraints = RigidbodyConstraints2D.None;
            Deactive();
            Vector2 dir = Vector2.zero;
            if (_player.transform.position.x > transform.position.x)
                dir = new Vector2(1, 1);
            else
                dir = new Vector2(-1, 1);

            _player.Hit();

            _player.rig.AddForce(dir * 1000);
            _player.rig.AddTorque(1000);
            return;
        }

        Enermy _enermy = collision.collider.GetComponent<Enermy>();

        if(_enermy != null)
        {
            _enermy.rig.constraints = RigidbodyConstraints2D.None;
            Deactive();
            Vector2 dir = Vector2.zero;
            if (_enermy.transform.position.x > transform.position.x)
                dir = new Vector2(1, 1);
            else
                dir = new Vector2(-1, 1);

            _enermy.Hit();

            _enermy.rig.AddForce(transform.forward * 1000);
            _enermy.rig.AddTorque(1000);
            return;
        }

        hitCount++;
        if (hitCount >= endHitCount)
            Deactive();
        else
            audioSource.PlayOneShot(hitClip);
    }

    private IEnumerator DeleteTime()
    {
        yield return MainSystem.Instance.CoroutineManager.WaitForSecond(deleteTime);
        Deactive();
    }
}