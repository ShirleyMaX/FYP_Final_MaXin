using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControl : MonoBehaviour
{
    public GameObject[] FurnitureType;
    public GameObject[] Furnitures;
    public ObjectManipulator ObjectManipulator;
    public GameObject UIPos;

    private GameObject _curFur;
    private int _length;
    private GameObject _instance;


    private void Start()
    {
        Furnitures = new GameObject[_length];
        _curFur = FurnitureType[0];
    }
    private void OnEnable()
    {
        Events.furnituretypeClicked += ShowFurList;
        Events.furnitureClicked += SpawnObj;

    }
    private void OnDisable()
    {
        Events.furnituretypeClicked -= ShowFurList;
        Events.furnitureClicked -= SpawnObj;
    }


    private void ShowFurList(int furInt)
    {
        _length = FurnitureType[furInt].transform.childCount;
        Furnitures = new GameObject[_length];

        for (int i = 0; i < _length; i++)
        {
            Furnitures[i] = FurnitureType[furInt].transform.GetChild(i).gameObject;
        }

        if (_curFur != null)
        {
            _curFur.SetActive(false);
        }
        _curFur = FurnitureType[furInt];
        _curFur.SetActive(true);
    }

   private void SpawnObj(GameObject ob)
    {
        _instance = Instantiate(ob, UIPos.transform.position, Quaternion.identity);
        Events.OnSpawnObj(_instance);
    }



}

