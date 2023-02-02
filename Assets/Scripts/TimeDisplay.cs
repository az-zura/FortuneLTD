using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeDisplay : MonoBehaviour
{
    public GameLoop gameLoop;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color workTimeColor;
    [SerializeField]
    private Color midnightColor;
    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int d = gameLoop.GetDay();
        int h = gameLoop.GetHour();
        int min = gameLoop.GetMinutes();
        bool work = gameLoop.IsWorkingTime();

        int someRandomStartDay = 4042;
        
        text.text = $"Montag {d + someRandomStartDay} - {(h > 9 ? h.ToString() : "0" + h)}:{(min > 9 ? min.ToString() : "0" + min)} Uhr \n{(h == 0 ? ("(Geisterstunde - freiwillige Mittagspause)") : (work ? "(Arbeitszeit)" : "(Freizeit)"))}";
        text.color = h == 0 ? midnightColor : (work ? workTimeColor : defaultColor);
    }
}
