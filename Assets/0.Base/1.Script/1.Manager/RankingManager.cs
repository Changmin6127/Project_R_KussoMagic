namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    public partial class RankingManager : MonoBehaviour //New Data Field
    {

    }


    public partial class RankingManager : MonoBehaviour //Data Field
    {
        public bool RankingOn = false;
        private NicknameData nicknameData;
        private RankData rankData;

        public List<RankUser> rankUsers = new List<RankUser>();

        const string URL = "https://docs.google.com/spreadsheets/d/1fxRo3Qfeg1aU4k4V5Z11nywfJWpRaE2RWaf4ztQyQY4/export?format=tsv&range=A2:B1";
        //1fxRo3Qfeg1aU4k4V5Z11nywfJWpRaE2RWaf4ztQyQY4
    }

    public partial class RankingManager : MonoBehaviour //Function Field
    {
        public void AccountPost()
        {

            WWWForm form = new WWWForm();
            form.AddField("order", "register");
            //form.AddField("nickname", MainSystem.Instance.UserDataManager.nickname);
            //form.AddField("email", MainSystem.Instance.UserDataManager.mail);
            form.AddField("score", 0);

            StartCoroutine(nicknamePost(form));
        }
        IEnumerator nicknamePost(WWWForm form)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
            {
                yield return www.SendWebRequest();

                if (www.isDone)
                {
                    nicknameData = JsonUtility.FromJson<NicknameData>(www.downloadHandler.text);
                    if (nicknameData.result == "OK")
                    {
                        Debug.Log("저장성공");
                    }
                }
                else
                {
                    Debug.Log("저장실패");
                }
            }
        }

        public void RankLoad()
        {
            rankUsers.Clear();
            WWWForm form = new WWWForm();
            form.AddField("order", "rankload");

            StartCoroutine(RankPost(form));
        }

        IEnumerator RankPost(WWWForm form)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
            {
                yield return www.SendWebRequest();

                if (www.isDone)
                {
                    print(www.downloadHandler.text);

                    rankData = JsonUtility.FromJson<RankData>(www.downloadHandler.text);

                    if (rankData.result == "OK")
                    {
                        RankingOn = true;

                        for (int index = 0; index < rankData.nick.Count; index++)
                        {
                            rankUsers.Add(new RankUser());
                            ChangeValue(rankUsers, index, rankData.nick[index], rankData.score[index]);
                        }
                        RankSort();
                    }
                }
                else
                {
                    RankingOn = false;
                }
            }
        }

        public void RankRefresh(RankUser rankUser)
        {
            for (int index = 0; index < rankUsers.Count; index++)
            {
                if (rankUsers[index].nickname == rankUser.nickname)
                {
                    ChangeValue(rankUsers, index, rankUser.nickname, rankUser.score);
                    RankSort();
                    return;
                }
            }

            rankUsers.Add(new RankUser());
            ChangeValue(rankUsers, rankUsers.Count - 1, rankUser.nickname, rankUser.score);
            RankSort();
        }
        private void ChangeValue(List<RankUser> rankUsers, int index, string nickname, int score)
        {
            RankUser temp = rankUsers[index];
            temp.nickname = nickname;
            temp.score = score;
            rankUsers[index] = temp;
        }

        private void RankSort()
        {
            rankUsers.Sort(delegate (RankUser x, RankUser y)
            {
                return y.score.CompareTo(x.score);
            });
        }
    }

    [System.Serializable]
    public class NicknameData
    {
        public string result;
        public string msg;
    }

    [System.Serializable]
    public class RankData
    {
        public string result;
        public string msg;
        public List<string> nick;
        public List<int> score;
    }
    [System.Serializable]
    public struct RankUser
    {
        public string nickname;
        public string mail;
        public int score;
    }
}