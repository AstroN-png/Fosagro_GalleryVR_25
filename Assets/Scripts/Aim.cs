using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    public GameObject dotPrefab;
    private GameObject dot;
    private Person person;
    private PositionPoint positionPoint;
    private BtnMenu btnMenu;
    private Filial filial;

    private void LateUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (dot == null)
            {
                dot = Instantiate(dotPrefab);
            }

            dot.transform.position = hit.point;


            if (hit.transform.GetComponent<BtnMenu>() == null &&
                hit.transform.GetComponent<Person>() == null &&
                hit.transform.GetComponent<PositionPoint>() == null &&
                hit.transform.GetComponent<Filial>() == null
                )
            {
                NullifyAllElements();
                return;
            }

            if (btnMenu == null)
            {
                if (hit.transform.GetComponent<BtnMenu>() != null)
                {
                    NullifyAllElements();
                    btnMenu = hit.transform.GetComponent<BtnMenu>();
                    return;
                }
            }
            else
            {
                btnMenu.SelectBtn();
                return;
            }

            if (filial == null)
            {
                if (hit.transform.GetComponent<Filial>() != null)
                {
                    NullifyAllElements();
                    filial = hit.transform.GetComponent<Filial>();
                    return;
                }
            }
            else
            {
                filial.SelectBtn();
                return;
            }

            if (person == null)
            {
                if (hit.transform.GetComponent<Person>() != null)
                {
                    NullifyAllElements();
                    person = hit.transform.GetComponent<Person>();
                    return;
                }
            }
            else
            {
                person.SelectPerson();
                return;
            }
            
            

            if (positionPoint == null)
            {
                if (hit.transform.GetComponent<PositionPoint>() != null)
                {
                    NullifyAllElements();
                    positionPoint = hit.transform.GetComponent<PositionPoint>();
                    return;
                }
            }
            else
            {
                positionPoint.SelectPosition();
                return;
            }



            NullifyAllElements();
        }
        else
        {
            NullifyAllElements();
            if (dot != null)
            {
                Destroy(dot);

            }
        }
    }


    void NullifyAllElements()
    {
        if (person != null)
        {
            person.UnselectPerson();
            person = null;
        }
        
        if (positionPoint != null)
        {
            positionPoint.UnselectPosition();
            positionPoint = null;
        }
        if (btnMenu != null)
        {
            btnMenu.UnselectBtn();
            btnMenu = null;
        }
        if (filial != null)
        {
            filial.UnselectBtn();
            filial = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Person>() != null)
        {
            collision.transform.GetComponent<Person>().pivot.SetActive(false);
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetComponent<Person>() != null)
        {
            collision.transform.GetComponent<Person>().pivot.SetActive(true);

        }
    }

}
