namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class UserDataManager : MonoBehaviour //Data Field
    {
        private int score = 0;
        private string nickname = "";
        private string mail = "None";
    }
    public partial class UserDataManager : MonoBehaviour //Initialize Function Field
    {
        public void Initialize()
        {

        }
    }

    public partial class UserDataManager : MonoBehaviour //GetSet Function Field
    {
        private void ChangeValue(List<RankUser> rankUsers, int index, string nickname, string mail, int score)
        {
            RankUser temp = rankUsers[index];
            temp.nickname = nickname;
            temp.score = score;
            rankUsers[index] = temp;
        }
    }
}