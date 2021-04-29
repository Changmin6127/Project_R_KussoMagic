using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil;
using UnityEngine.Events;

public partial class Player : MonoBehaviour  //Data Field
{ 
    //마우스의 위치에 따라 캐릭터를 좌 우로 방향을 변경해준다
    private enum Direction { Left, Right }
    private Direction playerDirection = Direction.Left;
    public bool isDie { get; private set; } = false;
    private bool isDrag = false;
    private bool isMagicReady = false;
    private float deltaTime = 0;
    private float chargyEnergy = 0;
    private float maxHandRotation = 20;
    public Rigidbody2D rig { get; private set; }

    [SerializeField]
    private Transform playerbodyTransform;
    [SerializeField]
    private Transform playerArmTransform;
    [SerializeField]
    private Transform playerHandTransform;
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

public partial class Player : MonoBehaviour  //Main Function Field
{
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdateCoroutine());
    }
    private void Update()
    {
        if (isDie || magicFire.isActive || MainSystem.Instance.SceneManager.GameScene.isNonPlayerControll)
            return;
        
        if(isDrag)
            Charge();

        if (Input.GetMouseButtonUp(0))
        {
            LeftClickUp();
            return;
        }     

        if (Input.GetMouseButtonDown(0))
            LeftClickDown();

        PlayerBodyDirection();

        if(isDrag)
            PlayerArmRotation();
    }
}

public partial class Player : MonoBehaviour  //Property Function Field
{
    public void Hit()
    {
        MainSystem.Instance.SceneManager.GameScene.isNonPlayerControll = true;
        isDie = true;
        hitEvent?.Invoke();
        explosionParticle.transform.position = transform.position;
        explosionParticle.Play(true);
    }
    private void Charge()
    {
        deltaTime += Time.deltaTime;

        if(deltaTime > 1.0f)
        {
            isMagicReady = true;
        }

        if (chargyEnergy > 1)
        {
            chargyEnergy = 1;
        }
        else
        {
            chargyEnergy += Time.deltaTime * 0.2f;
        }
    }
    private void LeftClickUp()
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
        playerArmTransform.localRotation = Quaternion.Euler(-22.18f, -78.648f, -10.948f);
    }

    private void LeftClickDown()
    {
        isMagicReady = false;
        deltaTime = 0;
        chargyEnergy = 0;
        magicHandEffect.SetActive(true);
        isDrag = true;
    }

    private void PlayerArmRotation()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(playerArmTransform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        playerArmTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        switch (playerDirection)
        {
            case Direction.Left: playerArmTransform.Rotate(new Vector3(0, 0f, 13f));  break;
            case Direction.Right: playerArmTransform.Rotate(new Vector3(180f, 0f, 13f)); break;
        }
    }

    private void PlayerBodyDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        switch (playerDirection)
        {
            case Direction.Left:
                if (mousePosition.x > playerbodyTransform.position.x)
                {
                    playerDirection = Direction.Right;
                    playerbodyTransform.localRotation = Quaternion.Euler(0, 120, 0);
                }
                break;

            case Direction.Right:
                if (mousePosition.x < playerbodyTransform.position.x)
                {
                    playerDirection = Direction.Left;
                    playerbodyTransform.localRotation = Quaternion.Euler(0, 240, 0);
                }
                break;
        }

    }
}
public partial class Player : MonoBehaviour  //Coroutine Function Field
{
    private IEnumerator UpdateCoroutine()
    {
        while (isDie == false)
        {
            yield return MainSystem.Instance.CoroutineManager.WaitForSecond(0.07f);
            if (isDrag == true)
            {
                float randomHandRotation = Random.Range(-maxHandRotation, maxHandRotation);
                playerHandTransform.localRotation = Quaternion.Euler(-50.692f, -12.437f, randomHandRotation * chargyEnergy);
            }
        }
    }
}