namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public partial class RankingScene : BaseScene   //Data Field
    {
        [SerializeField]
        private Text nicknameInputError;
        [SerializeField]
        private InputField nicknameInputField;
        [SerializeField]
        private List<Text> nicknames = new List<Text>();
        [SerializeField]
        private List<Text> scores = new List<Text>();
        [SerializeField]
        private UnityEvent nicknameSuccessEvent;
        [SerializeField]
        private Text myNickname;
        [SerializeField]
        private Text myScore;
    }

    public partial class RankingScene : BaseScene   //Function Field
    {
        public override void Initialize()
        {
            base.Initialize();
            RankRefresh();
        }

        public void RankRefresh()
        {
            RankUser user = new RankUser();
            //user.nickname = MainSystem.Instance.UserDataManager.nickname;
            //user.score = MainSystem.Instance.UserDataManager.score;
            MainSystem.Instance.RankingManager.RankRefresh(user);
            SetRanking();
        }




        private void SetRanking()
        {
            for (int index = 0; index < nicknames.Count; index++)
            {
                if (MainSystem.Instance.RankingManager.rankUsers.Count > index)
                {
                    nicknames[index].text = MainSystem.Instance.RankingManager.rankUsers[index].nickname;
                    scores[index].text = MainSystem.Instance.RankingManager.rankUsers[index].score.ToString();
                }
                else
                {
                    nicknames[index].text = "";
                    scores[index].text = "";
                }
            }
        }
    }

}