using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject[] mainUIs;
    public GameObject[] objList;
    public GameObject model;
    public GameObject LoginUI;
    public GameObject SAUI;
    public GameObject HelpUI;
    public GameObject UIPos;
    public GameObject SelectionUI;

    private GameObject _curMainUI;
    private GameObject _curObjUI;
    //private SpacialAwarenessControl SpacialAwarenessControl;
    private GameObject _instance;


    /// <summary>
    /// Applied in Open House
    /// Guest Login, wont use any Login configuration
    /// </summary>
    /// \
    private void Awake()
    {
        //SpacialAwarenessControl = FindObjectOfType<SpacialAwarenessControl>();
    }
    private void Start()
    {

        LoginUI.SetActive(false);
        SAUI.SetActive(true);
    }

    private void OnEnable()
    {
        Events.OpenListCliked += ShowList;

    }
    private void Update()
    {
        Debug.developerConsoleVisible = false;
    }

    private void OnDisable()
    {

        Events.OpenListCliked -= ShowList;
    }

    private void ShowList()
    {
        _curMainUI.SetActive(true);
    }
    public void OnClickGuestLogin()
    {
        //Hide LoginUI
        /*LoginUI.SetActive(false);
        model.SetActive(true);*/

    }

    /// <summary>
    /// Open House not in use
    /// Will link to database
    /// </summary>
    public void OnClickMainUIs(int num)
    {
        if (_curMainUI != null)
        {
            _curMainUI.SetActive(false);
        }
        _curMainUI = mainUIs[num + 1];
        _curMainUI.SetActive(true);
    }

    public void OnClickObjList(int num)
    {
/*        if (_curObjUI != null)
        {
            _curObjUI.SetActive(false);
        }
        _curObjUI = objList[num];
        _curObjUI.SetActive(true);*/

        Events.OnClickObjType(num);
    }

    public void OnClickIndivFur(GameObject obj)
    {
        _curMainUI.SetActive(false);
        Events.OnClickFurniture(obj);
       // _instance = Instantiate(SelectionUI, UIPos.transform.position, Quaternion.identity);
    }

    public void OnConfirmObserving()
    {
        //SpacialAwarenessControl.StopObserving();
        _curMainUI = mainUIs[0];
        _curObjUI = objList[0];

        LoginUI.SetActive(true);
        SAUI.SetActive(false);
    }

    public void OnSpeakHelp()
    {
        HelpUI.SetActive(true);
    }

    public void OnCloseHelp()
    {
        HelpUI.SetActive(false);
    }
}
