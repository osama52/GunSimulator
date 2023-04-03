using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentModel;
    public  List<GameObject> GunModels;
    public GameObject collectionPanel;
    public GameObject gunDisplayPanel;
    public Toggle Hide;
    public Toggle Hold;
    public Toggle Single;
    public Toggle Shake;
    private void Awake()
    {
        instance= this;
    }
    public void OpenCollectionPanel()
    {
        collectionPanel.SetActive(true);
        Destroy(currentModel);
    }
    
    public void HideUI()
    {
        if(Hide.isOn)
        {
            gunDisplayPanel.transform.GetChild(0).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(1).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(2).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            gunDisplayPanel.transform.GetChild(0).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(1).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(2).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(true);
        }
       
    }
    public void Fire()
    {
        print("sgdsg");
        if(Hold.isOn)
        {

        }
        if(Single.isOn)
        {
            currentModel.GetComponent<Animator>().Play("Shoot");
            //currentModel.GetComponent<AudioSource>().Play();
        }
        if(Shake.isOn)
        {

        }
    }
    public void G1()
    {
        currentModel = Instantiate(GunModels[0]);
        currentModel.transform.position= new Vector3(-0.5f,-1f,0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 8;

        collectionPanel.SetActive(false);
    }

    public void OnHold()
    {
        if (Hold.isOn)
        {
            Single.isOn = false;
            Shake.isOn = false;
        }
    }
    public void OnSingle()
    {
        if (Single.isOn)
        {
            Hold.isOn = false;
            Shake.isOn = false;
        }
    }
    public void OnShake()
    {
        if (Shake.isOn)
        {
            Single.isOn = false;
            Hold.isOn = false;
        }
    }
}
