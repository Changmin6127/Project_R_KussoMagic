namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using System;

    public partial class BaseScene : MonoBehaviour  //Data Field
    {
        [SerializeField]
        private UnityEvent sceneStartEvent;
    }

    public partial class BaseScene : MonoBehaviour  //Function Field
    {
        public void Start()
        {
            MainSystem.Instance.SceneManager.SignupCurrentScene(this);
        }
        public virtual void Initialize()
        {
            sceneStartEvent?.Invoke();
        }
    }

}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 터치입력 : MonoBehaviour {

    public int 적의명수; //0일시 클리어

    public GameObject 마법발사;
    public GameObject PlayerPar;

    public GameObject 마법중;
    WaitForSeconds 영점일 = new WaitForSeconds(0.05f);
    WaitForSeconds 이 = new WaitForSeconds(2);

    bool 터치중;
    float 랜덤팔위치;
    public Transform 팔위치;

    public GameObject 손왼쪽;
    public GameObject 손오른쪽;

    public bool 주인공먼저죽음;
    public GameObject 패배창;
    public bool 게임끝;
    private void Start()
    {
        StartCoroutine("업데이트");
    }

    
    void Update()
    {
        if(게임끝 == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                마법발사.SetActive(true);
                마법중.SetActive(false);
                터치중 = false;
                PlayerPar.transform.localRotation = Quaternion.Euler(0, 0, 80);
            }
            Vector3 pos = Camera.main.WorldToScreenPoint(PlayerPar.transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            PlayerPar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            PlayerPar.transform.Rotate(Vector3.forward * 180);

            if (PlayerPar.transform.rotation.eulerAngles.z > 90.0f && PlayerPar.transform.rotation.eulerAngles.z < 270.0f)
            {
                손오른쪽.SetActive(true);
                손왼쪽.SetActive(false);
            }
            else
            {
                손왼쪽.SetActive(true);
                손오른쪽.SetActive(false);
            }

            if (Input.GetMouseButton(0)) //터치중일때
            {
                if (마법발사.activeSelf == false)
                {
                    마법중.SetActive(true);
                }
                터치중 = true;
            }
            else
            {
                마법중.SetActive(false);
                터치중 = false;
                PlayerPar.transform.localRotation = Quaternion.Euler(0, 0, 80);
            }
        }
    }   
    
    public void 게임끝남()
    {
        게임끝 = true;
        StopCoroutine("업데이트");
    }

    IEnumerator 업데이트()
    {
        while (주인공먼저죽음 == false)
        {
            yield return 영점일;
            if (터치중 == true)
            {
                랜덤팔위치 = Random.Range(-10, 11);
                팔위치.localRotation = Quaternion.Euler(0, 0, 랜덤팔위치);
            }
        }     
    }

    void OnCollisionEnter2D(Collision2D 콜)
    {
        if (콜.gameObject.tag == "마법")
        {       
            주인공먼저죽음 = true;
            StopCoroutine("업데이트"); //마법쪽에서도 콜린더가 발동함 동시 발동되는지 한쪽만 발동될지는 미지수
            if (적의명수 > 0)
            {
                게임끝남();
                StartCoroutine("잠시후실패");
            }
        }
    }

    IEnumerator 잠시후실패()
    {
        yield return 이;
        패배창.SetActive(true);
    }
}

 * */