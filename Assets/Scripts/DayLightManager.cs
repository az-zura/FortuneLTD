using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DayLightManager : MonoBehaviour
{
    public Light sun;
    public Light moon;



    public PostProcessVolume globalPostProcessDay;
    public PostProcessVolume globalPostProcessNight;
    
    public float sunriseStart = 5;
    public float sunriseEnd = 7;
    
    public float sunsetStart = 17;
    public float sunsetEnd = 19;

    private Quaternion defaultOrientation;
    private Quaternion defaultOrientationMoon;

    public GameLoop gameLoop;
    

    // Start is called before the first frame update
    void Start()
    {
        defaultOrientation = sun.transform.rotation;
        defaultOrientation.eulerAngles =
            new Vector3(-90, defaultOrientation.eulerAngles.y, defaultOrientation.eulerAngles.z);
        defaultOrientationMoon = Quaternion.Inverse(defaultOrientation);
        
        sun.transform.rotation = defaultOrientation;
        moon.transform.rotation = defaultOrientationMoon;

        gameLoop.HourUpdated += HourChanged;
        
        loadNightSettings();

    }

    // Update is called once per frame
    void Update()
    {
        var timeOfDay = gameLoop.GetTime();
        var angle = Quaternion.Euler((timeOfDay / 24) * 360, 0, 0);
        //set sun rotation
        sun.transform.rotation = defaultOrientation * angle;
        moon.transform.rotation = defaultOrientationMoon * angle;

        if (timeOfDay > sunsetStart && timeOfDay < sunsetEnd)
        {
            sunset((timeOfDay - sunsetStart) / (sunsetEnd - sunsetStart));
        }        
        
        if (timeOfDay > sunriseStart && timeOfDay < sunriseEnd)
        {
            sunrise((timeOfDay - sunriseStart) / (sunriseEnd - sunriseStart));
        }
        
    }

    private void HourChanged(object sender, EventArgs args)
    {
        var hour = gameLoop.GetHour();
        if (hour == 6) //day starts 
        {
            loadDaySettings();
        }
        else if (hour == 18)
        { //day ends
            loadNightSettings();
        }
    }

    private void loadNightSettings()
    {
        sun.gameObject.SetActive(false);
        moon.gameObject.SetActive(true);
        
    }

    private void loadDaySettings()
    {
        sun.gameObject.SetActive(true);
        moon.gameObject.SetActive(false);
    }

    private void sunset(float progress)
    {
        globalPostProcessNight.weight = progress;
        globalPostProcessDay.weight = 1-progress;
    }

    private void sunrise(float progress)
    {
        globalPostProcessDay.weight = progress;
        globalPostProcessNight.weight = 1-progress;
    }
}