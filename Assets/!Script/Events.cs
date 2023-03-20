using System;
using UnityEngine;

public static class Events
{
    //UI Control Events
    public static Action<GameObject> furnitureClicked;
    public static void OnClickFurniture(GameObject obj) => furnitureClicked?.Invoke(obj);
    public static Action<GameObject> objSpawned;
    public static void OnSpawnObj(GameObject obj) => objSpawned?.Invoke(obj);

    public static Action<int> furnituretypeClicked;
    public static void OnClickObjType(int num) => furnituretypeClicked?.Invoke(num);

    public static Action OpenListCliked;
    public static void OnClickOpenList() => OpenListCliked?.Invoke();
}