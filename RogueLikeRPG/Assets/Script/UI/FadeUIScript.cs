using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeUIScript : MonoBehaviour
{
    Image myFadeImage;
    bool myIsFadingIn;
    float myFadeTime;
    float myFadeDeltaTime;
    // Use this for initialization
    void Start()
    {
        myFadeImage = GetComponentInChildren<Image>();
        myIsFadingIn = true;
        myFadeTime = 3;
        myFadeDeltaTime = myFadeTime;
        EventManager.StartListening(eEvent.FADE_EVENT, HandleFadeEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (myFadeDeltaTime > 0 && myIsFadingIn == true)
        {
            myFadeDeltaTime -= Time.deltaTime;
            if (myFadeDeltaTime < 0)
            {
                myFadeDeltaTime = 0;
            }
        }
        else
        {
            myFadeDeltaTime += Time.deltaTime;
            if (myFadeDeltaTime > myFadeTime)
            {
                myFadeDeltaTime = myFadeTime;
            }
        }


        float fadeValue = myFadeDeltaTime / myFadeTime;
        myFadeImage.color = new Color(0,0,0, fadeValue);
    }

    void HandleFadeEvent(BaseEvent anEvent)
    {
        FadeEvent fadeEvent = (FadeEvent)anEvent;
        myIsFadingIn = fadeEvent.myShouldFadeIn;
        myFadeTime = fadeEvent.myFadeTime;
        if (myIsFadingIn == true)
        {
            myFadeDeltaTime = myFadeTime;
        }
        else
        {
            myFadeDeltaTime = 0;
        }
    }
}
