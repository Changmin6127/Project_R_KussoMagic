using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public partial class SpeechStep : MonoBehaviour     //Data Field
{
    [SerializeField]
    private Canvas speechCanvas;
    [SerializeField]
    private Text speechText;
    [SerializeField]
    [TextArea]
    private List<string> speechs = new List<string>();
    [SerializeField]
    private List<UnityEvent> speechCallbackEvents = new List<UnityEvent>(); 
}

public partial class SpeechStep : MonoBehaviour     //Function Field
{
    public void Step(int _step)
    {
        speechText.text = "";
        speechCanvas.enabled = true;
        StartCoroutine(Typing(speechText, speechs[_step], 0.1f, _step));
    }

    public void StepFinishCallback(int _step)
    {
        speechCanvas.enabled = false;
        speechCallbackEvents[_step]?.Invoke();
    }

    IEnumerator Typing(Text typingText, string message, float speed, int step)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(3.0f);
        StepFinishCallback(step);
    }
}
