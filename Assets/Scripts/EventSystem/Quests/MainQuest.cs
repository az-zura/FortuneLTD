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
    [SerializeField] private Event10 event10;
    [SerializeField] private Event90 event90;
    [SerializeField] private Event30 event30;
    [SerializeField] private Event50 event50;
    [SerializeField] private Event70 event70;
    [SerializeField] private Event110 event110;
    [SerializeField] private Event130 event130;
    [SerializeField] private Event150 event150;
    [SerializeField] private Event170 event170;
    [SerializeField] private Event190 event190;


    [HideInInspector] public int mainQuestState = -1;

    public int getMainQuestState()
    {
        return mainQuestState;
    }

    public void setMainQuestState(int newState)
    {
        if (newState == mainQuestState) return;
        Debug.Log($"New state: {newState}");
        SaveGameManager.instance.SaveMainQuestState(newState);
        mainQuestState = newState;
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
                event30.gameObject.SetActive(true);
                break;
            case 30: //dialog zwischen hannah und joe bei der schule EVENT
                break;
            case 40://joe hat hannah in die schule gebracht und ist auf dem weg zur fortune ltd
                gameLoop.setDontSurpass(22);
                event50.gameObject.SetActive(true);
                break;
            case 50: //Konversation zwischen joe un karl EVENT
                break;
            case 60: //Konversation zwischen joe und karl ist vorbei, joe arbeitet   
                gameLoop.setDontSurpass(7);
                event70.gameObject.SetActive(true);
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
                event110.gameObject.SetActive(true);
                break;
            case 110: //EVENT Blumen incident
                break;
            case 120: //blument event fertig joe geht arbeiten
                gameLoop.setDontSurpass(8);
                event130.gameObject.SetActive(true);
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
                event170.gameObject.SetActive(true);
                break;
            case 170: //gespräch hannah und joe vor der schule
                break;
            case 180://joe in der arbeit
                gameLoop.setDontSurpass(8);
                event190.gameObject.SetActive(true);
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
        StartCoroutine(LateAwake()); // hopefully this way npcs start after time was set
    }

    public IEnumerator LateAwake()
    {
        yield return new WaitUntil(() => SaveGameManager.instance != null);
        
        gameLoop.setTime((int)SaveGameManager.instance.GetSavedTimePassedToday());
    }

    void Start()
    {
        setMainQuestState(SaveGameManager.instance.GetSavedMainQuestState());
        
        event10.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event10.uniqueEventName));
        event90.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event90.uniqueEventName));
        event30.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event30.uniqueEventName));
        event50.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event50.uniqueEventName));
        event70.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event70.uniqueEventName));
        event110.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event110.uniqueEventName));
        event130.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event130.uniqueEventName));
        event150.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event150.uniqueEventName));
        event170.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event170.uniqueEventName));
        event190.gameObject.SetActive(SaveGameManager.instance.WasEventCompleted(event190.uniqueEventName));
    }
}