using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SpeechStep : MonoBehaviour     //Data Field
{
    [SerializeField]
    private Canvas speechCanvas;
    [SerializeField]
    private Text speechText;
    [SerializeField]
    [TextArea]
    private List<string> speechs = new List<string>();
}

public partial class SpeechStep : MonoBehaviour     //Function Field
{
    public void Step(int value)
    {
        speechText.text = "";
        speechCanvas.enabled = true;
        StartCoroutine(Typing(speechText, speechs[value], 0.1f, value));
    }

    public void StepFinishCallback(int value)
    {
        speechCanvas.enabled = false;
    }

    IEnumerator Typing(Text typingText, string message, float speed, int step)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(2.5f);
        StepFinishCallback(step);
    }
}
