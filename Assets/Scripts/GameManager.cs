using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentModel;
    public  List<GameObject> GunModels;
    public GameObject collectionPanel;
    public GameObject gunDisplayPanel;
    public TextMeshProUGUI amoText;
    public Toggle Hide;
    public Toggle Hold;
    public Toggle Single;
    public Toggle Shake;
    public float shakeThreshold = 2f;
    Vector3 lastAcceleration;
    bool isShaking;
    bool hasShaken;
    bool isHold;
    float coolTime;
    bool single;

    int amo = 10;
    int r;
    private void Awake()
    {
        instance= this;
    }

    private void Update()
    {
        amoText.text = amo.ToString();
        if (Hold.isOn)
        {
            if(isHold )
            {
                if(single)
                {
                    if (Time.time > coolTime && amo > 0)
                    {
                        //Handheld.Vibrate();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        currentModel.GetComponent<AudioSource>().Play();
                        currentModel.GetComponent<Animator>().Play("Shoot");
                        coolTime = Time.time + 0.5f;
                        amo--;
                    }
                }
                else
                {
                    //Auto
                    if (Time.time > coolTime && amo > 0)
                    {
                        //Handheld.Vibrate();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        currentModel.GetComponent<AudioSource>().Play();
                        currentModel.GetComponent<Animator>().Play("AutoShoot");
                        coolTime = Time.time + 0.1f;
                        amo--;
                    }
                }
                
            }
        }
        if (Shake.isOn)
        {
            Vector3 acceleration = Input.acceleration;
            float deltaAcceleration = Vector3.Distance(acceleration, lastAcceleration);
            if (deltaAcceleration > shakeThreshold)
            {
                if (!isShaking && Time.time > coolTime )
                {
                    isShaking = true;
                    coolTime = Time.time + 0.4f;
                    if (!hasShaken && amo > 0) 
                    {
                        hasShaken = true;
                        Debug.Log("Device is shaking!");
                        //Handheld.Vibrate();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        currentModel.GetComponent<AudioSource>().Play();
                        currentModel.GetComponent<Animator>().Play("Shoot");
                        amo--;
                    }

                    
                }
            }
            else
            {
                isShaking = false;
            }
            if (Time.time > coolTime && hasShaken)
            {
                hasShaken = false;
            }
            lastAcceleration = acceleration;
        }
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
            gunDisplayPanel.transform.GetChild(1).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(2).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            gunDisplayPanel.transform.GetChild(1).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(2).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(true);
        }
       
    }
    public void SingleFire()
    {
      
        if(Single.isOn)
        {
            if(Time.time > coolTime && amo>0)
            {
                //Handheld.Vibrate();
                currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                currentModel.GetComponent<AudioSource>().Play();
                currentModel.GetComponent<Animator>().Play("Shoot");
                currentModel.GetComponent<Animator>().Play("Shoot");
                coolTime=Time.time+0.5f;
                amo--;
            }
           
        }
       
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
    public void OnPointerDown()
    {
        isHold = true;
    }
    public void OnPointerUp()
    {
        isHold = false;
    }
    public void Reload()
    {
        amo = r;
    }
    public void G0()
    {
        currentModel = Instantiate(GunModels[0]);
        currentModel.transform.position = new Vector3(-0.5f, -1f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 8;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo= r;
    }
    public void G1()
    {
        currentModel = Instantiate(GunModels[1]);
        currentModel.transform.position = new Vector3(-0.1f, -1f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 6;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo= r;
    }
    public void G2()
    {
        currentModel = Instantiate(GunModels[2]);
        currentModel.transform.position = new Vector3(-0.1f, -0.3f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G3()
    {
        currentModel = Instantiate(GunModels[3]);
        currentModel.transform.position = new Vector3(0.1f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 5;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G4()
    {
        currentModel = Instantiate(GunModels[4]);
        currentModel.transform.position = new Vector3(0f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 3.5f;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo = r;
    }
    public void G5()
    {
        currentModel = Instantiate(GunModels[5]);
        currentModel.transform.position = new Vector3(0.05f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 3.5f;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo = r;
    }
    public void G6()
    {
        currentModel = Instantiate(GunModels[6]);
        currentModel.transform.position = new Vector3(-0.1f, -0.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 3f;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo = r;
    }
}
