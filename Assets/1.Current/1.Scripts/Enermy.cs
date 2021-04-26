using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil;
using UnityEngine.Events;

public partial class Enermy : MonoBehaviour  //Data Field
{ 
    //마우스의 위치에 따라 캐릭터를 좌 우로 방향을 변경해준다
    private enum Direction { Left, Right }
    private Direction enermyDirection = Direction.Left;
    public bool isDie { get; private set; } = false;
    private bool isDrag = false;
    private bool isMagicReady = false;
    private float deltaTime = 0;
    private float chargyEnergy = 0;
    private float maxHandRotation = 30;
    public Rigidbody2D rig { get; private set; }

    [SerializeField]
    private Transform fireGuideTransform;
    [SerializeField]
    private Transform enermybodyTransform;
    [SerializeField]
    private Transform enermyArmTransform;
    [SerializeField]
    private Transform enermyHandTransform;
    [SerializeField]
    private GameObject magicHandEffect;
    [SerializeField]
    private MagicFire magicFire;
    [SerializeField]
    private Transform magicGuide;
    [SerializeField]
    private Transform magicGuideDestination;
    [SerializeField]
    private ParticleSystem explosionParticle;
    [SerializeField]
    private UnityEvent hitEvent;
}

public partial class Enermy : MonoBehaviour  //Main Function Field
{
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdateCoroutine());
    }
    private void Update()
    {
        if (isDie || magicFire.isActive)
            return;

        if(isDrag)
            Charge();

        EnermyBodyDirection();

        if(isDrag)
            EnermyArmRotation();
    }
}

public partial class Enermy : MonoBehaviour  //Property Function Field
{
    public void Hit()
    {
        isDie = true;
        hitEvent?.Invoke();
        explosionParticle.transform.position = transform.position;
        explosionParticle.Play(true);
    }

    private void Charge()
    {
        deltaTime += Time.deltaTime;

        if(deltaTime > 3.0f)
        {
            isMagicReady = true;
            AttackFire();
        }

        if (chargyEnergy > 1)
        {
            chargyEnergy = 1;
        }
        else
        {
            chargyEnergy += Time.deltaTime * 0.3f;
        }
    }
    private void AttackFire()
    {
        if (isMagicReady)
        {
            isMagicReady = false;
            magicFire.transform.position = magicGuide.position;
            magicFire.transform.LookAt(magicGuideDestination);
            magicFire.Active(chargyEnergy);
        }

        magicHandEffect.SetActive(false);
        isDrag = false;
        enermyArmTransform.localRotation = Quaternion.Euler(-22.18f, -78.648f, -10.948f);
    }

    public void AttackStart()
    {
        isMagicReady = false;
        deltaTime = 0;
        chargyEnergy = 0;
        magicHandEffect.SetActive(true);
        isDrag = true;
    }

    private void EnermyArmRotation()
    {
        Vector3 pos = enermyArmTransform.position;
        Vector3 dir = fireGuideTransform.position - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        enermyArmTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        switch (enermyDirection)
        {
            case Direction.Left: enermyArmTransform.Rotate(new Vector3(0, 0f, 10f));  break;
            case Direction.Right: enermyArmTransform.Rotate(new Vector3(180f, 0f, 10f)); break;
        }
    }

    private void EnermyBodyDirection()
    {
        Vector3 firePosition = fireGuideTransform.position;

        switch (enermyDirection)
        {
            case Direction.Left:
                if (firePosition.x > enermybodyTransform.position.x)
                {
                    enermyDirection = Direction.Right;
                    enermybodyTransform.localRotation = Quaternion.Euler(0, 120, 0);
                }
                break;

            case Direction.Right:
                if (firePosition.x < enermybodyTransform.position.x)
                {
                    enermyDirection = Direction.Left;
                    enermybodyTransform.localRotation = Quaternion.Euler(0, 240, 0);
                }
                break;
        }

    }
}
public partial class Enermy : MonoBehaviour  //Coroutine Function Field
{
    private IEnumerator UpdateCoroutine()
    {
        while (isDie == false)
        {
            yield return MainSystem.Instance.CoroutineManager.WaitForSecond(0.05f);
            if (isDrag == true)
            {
                float randomHandRotation = Random.Range(-maxHandRotation, maxHandRotation);
                enermyHandTransform.localRotation = Quaternion.Euler(-50.692f, -12.437f, randomHandRotation * chargyEnergy);
            }
        }
    }
}