using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil;

public partial class Player : MonoBehaviour  //Data Field
{ 
    //마우스의 위치에 따라 캐릭터를 좌 우로 방향을 변경해준다
    private enum Direction { Left, Right }
    private Direction playerDirection = Direction.Left;
    private bool isDie = false;
    private bool isDrag = false;
    [SerializeField]
    private Transform playerbodyTransform;
    [SerializeField]
    private Transform playerArmTransform;
    [SerializeField]
    private Transform playerHandTransform;
    [SerializeField]
    private GameObject magicHandEffect;
}

public partial class Player : MonoBehaviour  //Main Function Field
{
    private void Start()
    {
        StartCoroutine(UpdateCoroutine());
    }
    private void Update()
    {
        if (isDie)
            return;

        if (Input.GetMouseButtonUp(0))
            LeftClickUp();

        if (Input.GetMouseButtonDown(0))
            LeftClickDown();

        PlayerBodyDirection();

        if(isDrag)
        PlayerArmRotation();
    }
}

public partial class Player : MonoBehaviour  //Property Function Field
{
    private void LeftClickUp()
    {
        magicHandEffect.SetActive(false);
        isDrag = false;
        playerArmTransform.localRotation = Quaternion.Euler(0, 0, 80);
    }

    private void LeftClickDown()
    {
        magicHandEffect.SetActive(true);
        isDrag = true;
    }

    private void PlayerArmRotation()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(playerArmTransform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        playerArmTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //playerArmTransform.Rotate(Vector3.forward * 180);
    }

    private void PlayerBodyDirection()
    {
        Vector2 mousePosition = Camera.main.WorldToScreenPoint(Input.mousePosition);

        Debug.Log("Mouse");
        Debug.Log(mousePosition.x);
        Debug.Log("Player");
        Debug.Log(playerbodyTransform.position.x);
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
            yield return MainSystem.Instance.CoroutineManager.WaitForSecond(0.05f);
            if (isDrag == true)
            {
                float randomHandRotation = Random.Range(-10, 11);
                playerHandTransform.localRotation = Quaternion.Euler(0, 0, randomHandRotation);
            }
        }
    }
}