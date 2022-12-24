using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainManager : MonoBehaviour
{
    public ParticleSystem rain;
    public bool isPlay = false;
    public Text rainStatusText;

    FireScript f1;

    // Start is called before the first frame update
    public void Start()
    {
        rainStatusText.text = "Start";
    }

    public void rainStart()
    {
        if (isPlay)
        {
            rain.Stop();
            isPlay = false;
            rainStatusText.text = "Start";
        }
        else
        {
            rain.Play();
            isPlay = true;
            rainStatusText.text = "Stop";
            FireManager.Instance.OnBurnStopped(this.gameObject);
        }
    }
}
