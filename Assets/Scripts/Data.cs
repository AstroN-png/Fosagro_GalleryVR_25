using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data instance;

    public MainBase mainBase;
    public PersonBase personBase;

    private void Awake()
    {
        instance = this;
    }
}

[System.Serializable]
public class MainBase
{
    public Step[] step;
}

[System.Serializable]
public class Step
{
    public float timeForAction;
    public bool thisIsSpeach;
    public bool thisIsDescription;
    public bool StandUpAll;
    public bool sitDownAll;
    public bool StandUpOne;
    public bool SitDownOne;
    public bool thisIsHelpText;
    public bool goToCentrePosition; //положение в зале
    public bool goToDefaultPosition; //положение в зале
    public bool goToPererivPosition; //положение в зале
    public bool effectSoundActovate;
    public bool needToSelectPerson;
    public bool openResultPanel;
    public bool emptyAction;

    public string text;
    public int indexOfStep;
    public int indexOfPerson;

    public AudioClip speachClip;
    public AudioClip effectClip;
}

[System.Serializable]
public class PersonBase
{
    public Persons[] persons;
}

[System.Serializable]
public class Persons
{
    public int indexOfPerson;
    public string name;
    public Sprite photo;
}