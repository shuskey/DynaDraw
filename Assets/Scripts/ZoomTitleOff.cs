using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomTitleOff : MonoBehaviour
{
    public float timeToLive = 10;
    public float moveSpeed = 1.0f;
    float timeUnderAcceleration = 0f;
    public string effectName = "Slide";  // "Crawl" "Zoom" "Slide" "Fade"


    void Awake()
    {
        if (effectName == "Fade")
            SetTextToZeroAlpha(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (effectName == "Fade")
            StartCoroutine(FadeTextToFullAlpha(moveSpeed, gameObject));

        Destroy(gameObject, timeToLive);
        timeUnderAcceleration = 0f;

    }

    float AccelerationMultiplier(float time) => 0.1f * time * time;

    // Update is called once per frame
    void Update()
    {
        switch (effectName)
        {
            case "Crawl":
                this.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                break;
            case "Zoom":
                this.transform.Translate(0, 0, moveSpeed * AccelerationMultiplier(timeUnderAcceleration) * Time.deltaTime);
                break;
            case "Fade":
                break;
            case "Slide":
            default:
                this.transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                break;
        }
           

        timeUnderAcceleration += Time.deltaTime;

    }

    void SetTextToZeroAlpha(GameObject go)
    {
        var i = go.GetComponentInChildren<Text>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);        
    }

    public IEnumerator FadeTextToFullAlpha(float t, GameObject go)
    {
        var i = go.GetComponentInChildren<Text>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, GameObject go)
    {
        var i = go.GetComponentInChildren<Text>();

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
