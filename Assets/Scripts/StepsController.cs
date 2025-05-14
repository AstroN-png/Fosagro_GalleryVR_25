using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepsController : MonoBehaviour
{
    public static StepsController instance;

    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI mainPanelText;
    [SerializeField] TextMeshProUGUI personNameText;
    [SerializeField] Image photoOfPerson;
    [SerializeField] GameObject soundFrame;
    public Person[] person;
    [SerializeField] private int stepInProgress;
    AudioClip clip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
    }

    public void StartNewGame()
    {
        ScrollWithAudio.instance.PlaySound(false);
        stepInProgress = -1;
        AllPersonsToDefault();
        NextStep();
        GameController.instance.newGame = false;
    }

    void AllPersonsToDefault()
    {
        for (int i = 0; i < person.Length; i++)
        {
            person[i].ToDefault();
            person[i].SitDown();
            person[i].GoToDefaultPosition(true);
           
        }
    }

    void AllPersonsDisactivate()
    {
        for (int i = 0; i < person.Length; i++)
        {
            person[i].Disactivate();
            

        }
    }

    public void NextStep ()
    {
        stepInProgress++;
        AllPersonsDisactivate();
        print(stepInProgress);
        StopAllCoroutines();
        StartCoroutine(IENextStep());
    }

    IEnumerator IENextStep()
    {
        
        Step step = Data.instance.mainBase.step[stepInProgress];
        Persons[] persons = Data.instance.personBase.persons;

        //descriptionText.text = "";
        //mainPanelText.text = "";
        //personNameText.text = "";
        //photoOfPerson.gameObject.SetActive(false);
        //soundFrame.SetActive(false);
        yield return new WaitForSeconds(1);

        if (step.thisIsHelpText)
        {
            personNameText.text = persons[step.indexOfPerson].name;
            mainPanelText.text = step.text;
            photoOfPerson.sprite = persons[step.indexOfPerson].photo;
            photoOfPerson.gameObject.SetActive(true);
            soundFrame.SetActive(false);
            person[step.indexOfPerson].Selectable(true);
            yield break;
        }

        if (step.thisIsSpeach)
        {
            personNameText.text = persons[step.indexOfPerson].name;
            mainPanelText.text = step.text;
            photoOfPerson.sprite = persons[step.indexOfPerson].photo;
            soundFrame.SetActive(true);
            clip = step.speachClip;
            photoOfPerson.gameObject.SetActive(true);
            soundFrame.SetActive(true);
            person[step.indexOfPerson].ActivatePerson();
            ScrollWithAudio.instance.PlayAudioAndScroll(clip);
            yield break;

        }

        if (step.StandUpAll)
        {
            clip = step.effectClip;
            ScrollWithAudio.instance.PlayAudioAndScroll(clip);

            for (int i = 0; i < person.Length; i++)
            {
                person[i].StandUp();
                yield return new WaitForSeconds(0.2f);
            }
            
            yield break;
        }

        if (step.sitDownAll)
        {
            clip = step.effectClip;
            ScrollWithAudio.instance.PlayAudioAndScroll(clip);

            for (int i = 0; i < person.Length; i++)
            {
                person[i].SitDown();
                yield return new WaitForSeconds(0.2f);
            }

            yield break;
        }

        if (step.thisIsDescription)
        {
            descriptionText.text = step.text;
            ObjData.instance.OpenDescriptionPanel();
            NextStep();
            yield break;
        }

        if (step.goToCentrePosition)
        {
            person[step.indexOfPerson].GoToDefaultPosition(false);
            yield return new WaitForSeconds(4);
            NextStep();
            yield break;
        }

        if (step.goToDefaultPosition)
        {
            person[step.indexOfPerson].GoToDefaultPosition(true);
            yield return new WaitForSeconds(4);
            NextStep();
            yield break;
        }

        if (step.goToPererivPosition)
        {
            person[step.indexOfPerson].GoToPererivPosition();
            yield return new WaitForSeconds(8);
            NextStep();
            yield break;
        }

        if (step.SitDownOne)
        {
            person[step.indexOfPerson].SitDown();
            yield return new WaitForSeconds(2);
            NextStep();
            yield break;
        }

        if (step.StandUpOne)
        {
            person[step.indexOfPerson].StandUp();
            yield return new WaitForSeconds(2);
            NextStep();
            yield break;
        }

        if (step.openResultPanel)
        {
            ObjData.instance.OpenResultPanel();
            yield break;
        }

        if (step.emptyAction)
        {
            StartCoroutine(IEEmptyAction(step.timeForAction));
            yield break;
        }
    }

    IEnumerator IEEmptyAction(float time)
    {
        yield return new WaitForSeconds(time);
        NextStep();
    }

    
}
