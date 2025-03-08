//using UnityEngine;
//using UnityEngine.UI;

//public class FlickeringRawImage : MonoBehaviour
//{
//    public RawImage rawImage; // Assign this in the Inspector
//    public float minAlpha = 0.2f; // Minimum transparency
//    public float maxAlpha = 1.0f; // Maximum transparency
//    public float flickerSpeed = 2.0f; // Speed of flickering

//    void Update()
//    {
//        if (rawImage != null)
//        {
//            float alpha = Mathf.PingPong(Time.time * flickerSpeed, maxAlpha - minAlpha) + minAlpha;
//            SetTransparency(alpha);
//        }
//    }

//    private void SetTransparency(float alpha)
//    {
//        Color color = rawImage.color;
//        color.a = Mathf.Clamp01(alpha);
//        rawImage.color = color;
//    }
//}


//----------------VERSION 1----------------------


//using UnityEngine;
//using UnityEngine.UI;

//public class FlickeringRawImage : MonoBehaviour
//{
//    public RawImage rawImage; // Assign this in the Inspector
//    public float minAlpha = 0.0f; // Minimum transparency (darkest state)
//    public float maxAlpha = 1.0f; // Maximum transparency (brightest state)
//    public float minFlickerTime = 0.05f; // Minimum time between flickers
//    public float maxFlickerTime = 0.5f; // Maximum time between flickers
//    private float nextFlickerTime;
//    private bool isFlickering = false;

//    void Start()
//    {
//        // Initialize the first flicker time
//        SetNextFlickerTime();
//    }

//    void Update()
//    {
//        if (rawImage != null)
//        {
//            // If the time has come for the next flicker, we change the alpha
//            if (Time.time >= nextFlickerTime)
//            {
//                // Flicker randomly between minAlpha and maxAlpha
//                float alpha = Random.Range(minAlpha, maxAlpha);
//                SetTransparency(alpha);

//                // Set the next flicker time
//                SetNextFlickerTime();
//            }
//        }
//    }

//    private void SetTransparency(float alpha)
//    {
//        Color color = rawImage.color;
//        color.a = Mathf.Clamp01(alpha); // Ensure alpha stays between 0 and 1
//        rawImage.color = color;
//    }

//    private void SetNextFlickerTime()
//    {
//        // Set a random time interval for the next flicker
//        nextFlickerTime = Time.time + Random.Range(minFlickerTime, maxFlickerTime);
//    }
//}

//----------------------VERSION 2-----------------------------

using UnityEngine;
using UnityEngine.UI;

public class FlickeringRawImage : MonoBehaviour
{
    public RawImage rawImage; // Assign this in the Inspector
    public float maxAlpha = 1.0f; // Maximum transparency (on state)
    public float minFlickerTime = 0.1f; // Minimum time between flickers (off to on or on to off)
    public float maxFlickerTime = 0.5f; // Maximum time between flickers (off to on or on to off)
    private float nextFlickerTime;

    void Start()
    {
        // Initialize the first flicker time
        SetNextFlickerTime();
    }

    void Update()
    {
        if (rawImage != null)
        {
            // If the time has come for the next flicker, we change the alpha
            if (Time.time >= nextFlickerTime)
            {
                // Flicker randomly between off (alpha = 0) and on (alpha = maxAlpha)
                float alpha = Random.Range(0, 2) > 0.5f ? maxAlpha : 0f;
                SetTransparency(alpha);

                // Set the next flicker time
                SetNextFlickerTime();
            }
        }
    }

    private void SetTransparency(float alpha)
    {
        Color color = rawImage.color;
        color.a = Mathf.Clamp01(alpha); // Ensure alpha stays between 0 and 1
        rawImage.color = color;
    }

    private void SetNextFlickerTime()
    {
        // Set a random time interval for the next flicker (between minFlickerTime and maxFlickerTime)
        nextFlickerTime = Time.time + Random.Range(minFlickerTime, maxFlickerTime);
    }
}

