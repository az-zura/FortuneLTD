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
    [SerializeField] private GameObject event30Object;

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
            case 0:
                gameLoop.setTime(17);
                event10.InitializeEvent();
                break;
            case 10: break;
            case 20:
                event30Object.SetActive(true);
                break;
            case 30: break;
            case 40:
                break;

        }
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