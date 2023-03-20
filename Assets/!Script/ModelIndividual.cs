using System;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class ModelIndividual : MonoBehaviour
{
    public ObjectManipulator ObjectManipulator;
    public GameObject[] FunUIs;
    public GameObject TickUI;
    public GameObject[] UItags;
    public Outline OutlineScript;

    private bool _spawned;
    private bool _confirmedPos;
    private GameObject _spawnedObj;


    private void OnEnable()
    {
        Events.furnitureClicked += ToggleOutline;
        Events.objSpawned += GetSpawnedObj;

    }

    private void OnDisable()
    {

        Events.furnitureClicked -= ToggleOutline;
        Events.objSpawned -= GetSpawnedObj;
    }

    private void GetSpawnedObj(GameObject obj)
    {
        _spawnedObj = obj;
    }

    private void ToggleOutline(GameObject obj)
    {
        _spawned = true;
        Debug.Log("spawned");
    }

    private void OnMouseDown()
    {
        if (_confirmedPos)
        {
     
                OutlineScript.enabled = true;

                for (int i = 0; i < FunUIs.Length; i++)
                {
                    FunUIs[i].SetActive(true);
                }
                TickUI.SetActive(false);    
        }       
    }

    public void OnDelete()
    {
        Debug.Log("On Click delete");
        Destroy(gameObject);
        Events.OnClickOpenList();

        _confirmedPos = false;
    }

    public void OnModify()
    {
        Debug.Log("On Click modify");

        ObjectManipulator.enabled = true;
        //hide UI, show Tick
        for(int i =0; i < FunUIs.Length; i++)
        {
            FunUIs[i].SetActive(false);
        }
        TickUI.SetActive(true);

        _confirmedPos = false;
    }

    public void OnOpenList()
    {

        Events.OnClickOpenList();
    }

    public void OnConfirmFurPos()
    {
        ObjectManipulator.enabled = false;

        for (int i = 0; i < FunUIs.Length; i++)
        {
            FunUIs[i].SetActive(true);
        }
        TickUI.SetActive(false);
        

        _confirmedPos = true;
    }

    public void OnCloseUIs()
    {
        OutlineScript.enabled = false;

        for (int i = 0; i < FunUIs.Length; i++)
        {
            FunUIs[i].SetActive(false);
        }
        TickUI.SetActive(false);

        OnOpenList();
    }
}
