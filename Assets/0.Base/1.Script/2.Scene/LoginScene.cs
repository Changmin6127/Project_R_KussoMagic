namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;

    public partial class LoginScene : BaseScene //Data Field
    {
        private string nickname = "";

        [SerializeField]
        private Text nicknameInputError;
        [SerializeField]
        private UnityEvent nicknameSuccessEvent;
    }

    public partial class LoginScene : BaseScene //Function Field
    {
        public void NicknameCheck(string _nickname)
        {
            nickname = _nickname.Trim();

            if (nickname == "")
            {
                nicknameInputError.text = "한글자라도 입력하세요.";
            }
            else
            {
                //유저데이터매니저와 스프레드시트에 저장해주어야한다
                nicknameSuccessEvent?.Invoke();
            }
        }
    }
}