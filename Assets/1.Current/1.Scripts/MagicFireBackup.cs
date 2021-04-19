using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil;

public partial class MagicFireBackup : MonoBehaviour  //Data Field
{
    private GameObject 마법발사;

    public Transform playerArm;
    public Transform playerHand;

    public GameObject magicHandEffect;

    private bool isClick;
    private float randomHandRotation;

    public GameObject leftHand;
    public GameObject rightHand;

    public bool isDie = false;
  

    public void GameEnd()
    {
        isDie = true;
        StopAllCoroutines();
    }

    IEnumerator UpdateCoroutine()
    {
        while (isDie == false)
        {
            yield return MainSystem.Instance.CoroutineManager.WaitForSecond(0.05f);
            if (isClick == true)
            {
                randomHandRotation = Random.Range(-10, 11);
                playerHand.localRotation = Quaternion.Euler(0, 0, randomHandRotation);
            }
        }
    }
}

public partial class MagicFireBackup : MonoBehaviour  //Main Function Field
{
    private void Start()
    {
        StartCoroutine("UpdateCoroutine");
    }


    void Update()
    {
        if (isDie == false)
        {
            if (Input.GetMouseButtonUp(0))
                LeftClickUp();


            Vector3 pos = Camera.main.WorldToScreenPoint(playerArm.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            playerArm.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            playerArm.Rotate(Vector3.forward * 180);

            if (playerArm.rotation.eulerAngles.z > 90.0f && playerArm.rotation.eulerAngles.z < 270.0f) //드래그일때만
            {
                rightHand.SetActive(true);
                leftHand.SetActive(false);
            }
            else
            {
                leftHand.SetActive(true);
                rightHand.SetActive(false);
            }

            magicHandEffect.SetActive(false);
            isClick = false;
            playerArm.transform.localRotation = Quaternion.Euler(0, 0, 80);



            PlayerArmProgress();

            if (Input.GetMouseButton(0)) //터치중일때
                LeftClickDrag();
        }
    }
}

public partial class MagicFireBackup : MonoBehaviour  //Property Function Field
{
    private void LeftClickUp()
    {
        magicHandEffect.SetActive(false);
        isClick = false;
        playerArm.localRotation = Quaternion.Euler(0, 0, 80);
    }

    private void LeftClickDrag()
    {
        if (마법발사.activeSelf == false)
        {
            magicHandEffect.SetActive(true);
        }
        isClick = true;
    }

    private void PlayerArmProgress()
    {
     
    }
}