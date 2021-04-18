using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[System.Serializable]
public class RegenZone
{
    public double lat;
    public double lng;
    public int regenProbability;
    public int[] monsters;
}

[System.Serializable]
public class EventZone
{
    public double lat;
    public double lng;
    public int eventNumber;
}
public partial class GPSManager : MonoBehaviour     //GPS Check Data Field
{
    private double distance;
    private double radius = 6371;   //지구의 반지름 (km)
    private double toRadian = Mathf.PI / 180;

    private double deltaLatitude;
    private double deltaLongitude;

    private double sinDeltaLat;
    private double sinDeltaLng;
    private double squareRoot;

    private bool isActive = false;

    private List<RegenZone> testRegenZone = new List<RegenZone>();
    private List<EventZone> testEventZone = new List<EventZone>();
    private RegenZone currentRegenZone;
    private EventZone currentEventZone;

}

public partial class GPSManager : MonoBehaviour     //GPS Check Function Field
{
    public void Initialize()
    {
        StartCoroutine("DistanceCheckTime");
        MyGPSStart();
    }

    //미리 스프레드시트에 있는 값들을 가져와서 리스트에 저장해두고
    //그 저장해둔 리스트를 순회하는 방식으로간다 (이때 순회하는 리스트들을 몬스터리젠존, 이벤트존 따로 관리하기 편하게 나눠준다.)
    public void DistanceCheck()
    {
        bool eventZone = false;
        RefreshMyGPS();
        currentEventZone = CheckCurrentEventZone();


        if (currentEventZone != null)
            eventZone = true;
        else
            currentRegenZone = CheckCurrentRegenZone();


        if (eventZone)
        {
            //이벤트존에 있을경우 이벤트존 실행
            //10초에 한번씩 체크할때마다 해당 이벤트존에 안있을시 활성화 시켰던 버튼 비활성화 시켜야함
        }
        else if(currentRegenZone != null)
        {
            //이벤트존에 있지 않고 리젠존에 있을경우 리젠존 실행
      
            //RegenZone의 구성  위치(좌표), 리젠확률, 등장 몬스터 번호들(리스트)
        }


    }
    IEnumerator DistanceCheckTime()
    {
        while (true)
        {
            if (isActive)
            {
                DistanceCheck();
            }

            yield return new WaitForSeconds(10);
        }
    }

    private EventZone CheckCurrentEventZone()
    {
        for (int index = 0; index < testEventZone.Count; index++)
        {
            //아래의 두 거리를 측정한다.
            double value = distanceInKilometerByHaversine(current_Lat, current_Long, testEventZone[index].lat, testEventZone[index].lng);

            if (0.1f > value) //비율은 1당 1km
            {
                return testEventZone[index];
                //내가 들어와 있는 곳이 이벤트 존일경우 리젠존은 스킵한다.
                //내가 해당존에 들어와 있을경우 CurrentZone에 넣어준다 (그리하여 다른존과 겹쳐 있을경우에도 하나만 실행될수 있도록)
            }
        }
        return null;
    }
    private RegenZone CheckCurrentRegenZone()
    {
        for (int index = 0; index < testRegenZone.Count; index++)
        {
            //아래의 두 거리를 측정한다.
            double value = distanceInKilometerByHaversine(current_Lat, current_Long, testRegenZone[index].lat, testRegenZone[index].lng);

            if (0.1f > value) //비율은 1당 1km
            {
                return testRegenZone[index];
                //내가 해당존에 들어와 있을경우 CurrentZone에 넣어준다 (그리하여 다른존과 겹쳐 있을경우에도 하나만 실행될수 있도록)
            }
        }
        return null;
    }
}

public partial class GPSManager : MonoBehaviour     //MyGPS Field
{
    private double current_Lat; //현재 위도
    private double current_Long; //현재 경도

    private static bool gpsStarted = false;

    private static LocationInfo location;

    public void MyGPSStart()
    {
        StartCoroutine(Active());
    }
    IEnumerator Active()
    {
        //GPS가 없는 경우 (GPS가 없는 기기거나 안드로이드 GPS를 설정 하지 않았을 경우  (CodeV에선 제외했었음)
        if (Input.location.isEnabledByUser == false)
        {
            Debug.Log("GPS is not enabled");
            yield break;
        }

        //GPS 서비스 시작
        Input.location.Start();
        Debug.Log("Awaiting initialization");

        //활성화될 때 까지 대기
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1.0f);
            maxWait -= 1;
        }

        //20초 지날경우 활성화 중단
        if (maxWait < 1)
        {
            Debug.Log("Timed out"); //실패 로그 띄워주기 이경우 다시 시도 해야함
            yield break;
        }

        //연결 실패
        if (Input.location.status == LocationServiceStatus.Failed)  //연결 실패 했을경우 이경우에도 다시시도 해야함
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            //접근 허가됨, 최초 위치 정보 받아오기
            RefreshMyGPS();
            gpsStarted = true;
            isActive = true;
        }
    }

    public void RefreshMyGPS()
    {
        if (gpsStarted)
        {
            location = Input.location.lastData;
            current_Lat = location.latitude * 1.0d;
            current_Long = location.longitude * 1.0d;
        }
        else
        {
            //GPS 연결 끊김
        }
    }
    //위치 서비스 종료
    public void StopGPS()
    {
        if (Input.location.isEnabledByUser)
        {
            isActive = false;
            gpsStarted = false;
            Input.location.Stop();
        }
    }
}

public partial class GPSManager : MonoBehaviour     //Property Function Field
{
    private double distanceInKilometerByHaversine(double x1, double y1, double x2, double y2)
    {
        deltaLatitude = Math.Abs(x1 - x2) * toRadian;
        deltaLongitude = Math.Abs(y1 - y2) * toRadian;

        sinDeltaLat = Math.Sin(deltaLatitude / 2);
        sinDeltaLng = Math.Sin(deltaLongitude / 2);
        squareRoot = Math.Sqrt(
            sinDeltaLat * sinDeltaLat +
            Math.Cos(x1 * toRadian) * Math.Cos(x2 * toRadian) * sinDeltaLng * sinDeltaLng);

        distance = 2 * radius * Math.Asin(squareRoot);

        return distance;
    }
}