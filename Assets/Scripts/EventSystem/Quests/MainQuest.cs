using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class MainQuest : MonoBehaviour
{
    /*
    0 - spiel beginnt
    10 - erster tag, in der früh EVENT
    
    20 - erster dialog vorbei, joe geht mit hannah in die schule
    30 - dialog zwischen hannah und joe bei der schule EVENT
    40 - joe hat hannah in die schule gebracht und ist auf dem weg zur fortune ltd
    
    50 - Konversation zwischen joe un karl EVENT
    60 - Konversation zwischen joe und karl ist vorbei, joe arbeitet   
    */
    // Start is called before the first frame update

    [SerializeField] private GameLoop gameLoop;
    [SerializeField] private SequentialEvent event10;
    [SerializeField] private Event90 event90;
    [SerializeField] private GameObject event30Object;
    [SerializeField] private GameObject event50Object;
    [SerializeField] private GameObject event70Object;
    [SerializeField] private GameObject event110Object;
    [SerializeField] private GameObject event130Object;
    [SerializeField] private Event150 event150;
    [SerializeField] private GameObject event170Object;
    [SerializeField] private GameObject event190Object;


    private int mainQuestState = -1;

    public int getMainQuestState()
    {
        return mainQuestState;
    }

    public void setMainQuestState(int newState)
    {
        if (newState == mainQuestState) return;
        switch (newState)
        {
            case 0: //start
                event10.InitializeEvent();
                break;
            case 10: //erster tag, in der früh EVENT
                gameLoop.setDontSurpass(19);
                break;
            case 20://erster dialog vorbei, joe geht mit hannah in die schule
                gameLoop.setDontSurpass(20);
                event30Object.SetActive(true);
                break;
            case 30: //dialog zwischen hannah und joe bei der schule EVENT
                break;
            case 40://joe hat hannah in die schule gebracht und ist auf dem weg zur fortune ltd
                gameLoop.setDontSurpass(22);
                event50Object.SetActive(true);
                break;
            case 50: //Konversation zwischen joe un karl EVENT
                break;
            case 60: //Konversation zwischen joe und karl ist vorbei, joe arbeitet   
                gameLoop.setDontSurpass(7);
                event70Object.SetActive(true);
                break;
            case 70: //Konversation am abend zwischen pam und joe EVENT
                break;
            case 80: //nacht skippen
                gameLoop.setDontSurpass(19);
                gameLoop.setFastForwardUntil(18);
                event90.InitializeEvent();
                break;
            case 90: //EVENT : pam sagt hannah bleibt heute daheim
                break;
            case 100: // gesräch mit pam vorbei joe geht in die arbeit
                gameLoop.setDontSurpass(20.5f);
                event110Object.SetActive(true);
                break;
            case 110: //EVENT Blumen incident
                break;
            case 120: //blument event fertig joe geht arbeiten
                gameLoop.setDontSurpass(8);
                event130Object.SetActive(true);
                break;
            case 130: //gespräch ziwschen pam und joe am abend
                break;
            case 140: //gespräch ziwschen pam vorbei // nacht skippen
                gameLoop.setFastForwardUntil(17);
                gameLoop.setDontSurpass(19);
                event150.InitializeEvent();
                break;
            case 150: //gespräch zwishen pam und joe am nächsten mrogen, blume taucht auf
                break;
            case 160: //joe und hannah fliegen in die schule
                gameLoop.setDontSurpass(20);
                event170Object.SetActive(true);
                break;
            case 170: //gespräch hannah und joe vor der schule
                break;
            case 180://joe in der arbeit
                gameLoop.setDontSurpass(8);
                event190Object.SetActive(true);
                break;
            case 190:// gespräch zwischen joe und pam -> hauptquest endet hier !
                break;
            case 200:
                gameLoop.clearDontSurpass();
                gameLoop.setFastForwardUntil(17);
                break;
        }
    }

    //start of game has to be set before npcs start
    private void Awake() //game starts at 17:00
    {
        gameLoop.setTime(17);
    }

    void Start()
    {
        setMainQuestState(0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}