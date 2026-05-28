using TMPro;
using UnityEngine;

public class AnimationEventer : MonoBehaviour
{
    private TextMeshProUGUI currentText;

    public void TimerSystemStart(int i)
    {
        TimerSystem.instance.StartSegment(i);
    }

    public void TimerSystemEnd()
    {
        TimerSystem.instance.EndSegment();
    }

}
