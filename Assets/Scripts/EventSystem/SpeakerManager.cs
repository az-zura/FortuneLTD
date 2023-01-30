using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Actions;
using EventSystem.Base;
using NPC.NpcMovement;
using Unity.VisualScripting;
using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    // short conversations are conversations that are reactions to the player being near
    // they only consist of 1 person talking to the player
    public static SpeakerManager instance;
    public GameObject player;
    public Speechbubble speechbubble;
    
    [HideInInspector] public List<Speaker> speakers = new List<Speaker>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    /*
    // for small dialogues
    public Speaker rndNeighbour1 = new Speaker(1,new string[][]{new []{
        "Ich hab mir eine neue CD gekauft und sie spielt sogenannte Motorengeräusche… wunderschön.", 
        "Das erinnert mich an etwas… ich weiß nur nicht was…"}});
    public Speaker rndNeighbour2 = new Speaker(2,new string[][]{new []{
        "Hallo Joe! Hast du es schon gehört? Sie haben einen neuen Grauton entdeckt. Der ist jetzt voll im Trend."}});
    public Speaker rndNeighbour3 = new Speaker(3,new string[][]{new []{
        "Einen neutralen Abend wünsche ich dir. Ich glaube, heute soll es bewölkt werden."}});
    public Speaker rndNeighbour4 = new Speaker(4,new string[][]{new []{
        "Gestern wurde ich auf GhostMatch geghostet, ist das nicht toll?", "Vielleicht finde ich endlich den Partner für den Tod."}});
    
    public Speaker rndColleague1 = new Speaker(5,new string[][]{new []{
        "Einen traurigen Tag wünsche ich dir. Hast du die Dokumente von heute schon durchgearbeitet?",
        "Ich frage mich immer, was in dem zensierten Teil der Akte steht…"}, 
        new []{"Ich habe gehört, Karen soll befördert werden. Ich frage mich, wer ihren Job bekommt."}});
    public Speaker rndColleague2 = new Speaker(6,new string[][]{new []{
        "Ich frage mich, was Hubertus gemeint hat als er sagte, Man findet das Glück in der Weststraße.", 
        "Ist dort nicht schon ewig eine Baustelle? Und wozu braucht man schon Glück, nicht wahr?", 
        "Wir können schließlich auch arbeiten."}}); // Nach der Abschiedsfeier
    public Speaker rndColleague3 = new Speaker(7,new string[][]{new []{
        "Karen trackt unseren Fortschritt, ich sollte besser wieder weiterarbeiten…"}, 
        new []{"Findest du Karen nicht auch beängstigend. Es wundert mich nicht, dass sie Abteilungsleiter ist."}});
    
    public Speaker karl = new Speaker(8,new string[][]{new []{
        "Diesen Monat werde ich Mitarbeiter des Monats. Ich habe heute bereits eine Person in die Geisterwelt gebracht, und du?"}, 
        new []{"Ich muss mich bei Karen einschleimen, vielleicht werde ich dann endlich Mitarbeiter des Monats. Denkst du, sie mag ein Foto von mir?"},
        new []{"Solltest du nicht auch arbeiten?"}});
    public Speaker karen = new Speaker(9,new string[][]{new []{
        "Hallo Joe! Mach dich besser an die Arbeit. Ich habe ein neues Tool, um euren Fortschritt zu tracken."},
        new[] {"Karl nervt mich unglaublich mit seinen merkwürdigen Aktionen.", "Gestern hat er Flyer mit seinem Gesicht am Arbeitsdrucker gedruckt und ihn dabei zerstört.", "Ich musste den Hausmeister rufen."}, // an Tag 2?
        new []{"Was ist denn jetzt schon wieder?"}});
    
    // Nach Blumenevent auf der Straße
    public Speaker onStreet1 = new Speaker(10,new string[][]{new []{
            "Hast du die Verhaftung vorhin mitbekommen? Da hat jemand solche skurrilen grünen Gewächse gepflanzt.","Ich frage mich, wo er die her hatte…"}});
    public Speaker onStreet2 = new Speaker(11,new string[][]{new []{
            "Hast du das vorhin mitbekommen? Sie haben jemanden wegen Sachbeschädingung festgenommen.","Richtig so, man kann doch nicht einfach unsere Straßen verschandeln."}});
    
    // Just some more random Ghosts:
    public Speaker onStreet3 = new Speaker(12, new string[][]{new []{
        "Bald gehe ich ins Jenseits, darauf freue ich mich schon."},
        new []{"Sie sagen, im Jenseits gibt es Farben. Ich frage mich, wie das wohl ist…"}
    });
    public Speaker onStreet4 = new Speaker(13, new string[][]{new []{
        "Hast du es schon gehört? Die Fortune LTD möchte dieses Jahr noch mehr Geister erschaffen.","Man hört, dass es immer mehr Menschen auf der Erde gibt."}});
    public Speaker onStreet5 = new Speaker(14,new string[][]{new []{
        "Ich bin hier, um mir neue Zäune zu kaufen.","Es gibt immer mehr Geister-Küken auf dem Land. Ich frage mich, was da los ist…"}});
    public Speaker onStreet6 = new Speaker(15, new string[][]{new []{
        "Die Weststraße ist seit Jahren wegen Bauarbeiten gesperrt. Ich frage mich, was da los ist…","Es scheint so, als würden dort merkwürdige Gewächse die Abflüsse verstopfen."}});
    */
}
