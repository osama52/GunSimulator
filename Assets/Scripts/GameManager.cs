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
        r = 6;
        amo = r;
    }
    public void G7()
    {
        currentModel = Instantiate(GunModels[7]);
        currentModel.transform.position = new Vector3(-0.1f, -0.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G8()
    {
        currentModel = Instantiate(GunModels[8]);
        currentModel.transform.position = new Vector3(-0.1f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4f;
        single = true;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G9()
    {
        currentModel = Instantiate(GunModels[9]);
        currentModel.transform.position = new Vector3(-0.1f, -0.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 3.5f;
        single = true;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G10()
    {
        currentModel = Instantiate(GunModels[10]);
        currentModel.transform.position = new Vector3(-0.1f, -0.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G11()
    {
        currentModel = Instantiate(GunModels[11]);
        currentModel.transform.position = new Vector3(-0.1f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G12()
    {
        currentModel = Instantiate(GunModels[12]);
        currentModel.transform.position = new Vector3(0f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 0.6f;
        single = true;
        collectionPanel.SetActive(false);
        r = 6;
        amo = r;
    }
    public void G13()
    {
        currentModel = Instantiate(GunModels[13]);
        currentModel.transform.position = new Vector3(-0.2f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 0.6f;
        single = true;
        collectionPanel.SetActive(false);
        r = 6;
        amo = r;
    }
    public void G14()
    {
        currentModel = Instantiate(GunModels[14]);
        currentModel.transform.position = new Vector3(-0.2f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 0.8f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G15()
    {
        currentModel = Instantiate(GunModels[15]);
        currentModel.transform.position = new Vector3(-0.2f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1.3f;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo = r;
    }
    public void G16()
    {
        currentModel = Instantiate(GunModels[16]);
        currentModel.transform.position = new Vector3(-0.1f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4.5f;
        single = true;
        collectionPanel.SetActive(false);
        r = 8;
        amo = r;
    }
    public void G17()
    {
        currentModel = Instantiate(GunModels[17]);
        currentModel.transform.position = new Vector3(-0.1f, 0.1f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1.1f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G18()
    {
        currentModel = Instantiate(GunModels[18]);
        currentModel.transform.position = new Vector3(-0.3f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 5.5f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G19()
    {
        currentModel = Instantiate(GunModels[19]);
        currentModel.transform.position = new Vector3(-0.3f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * -0.03f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G20()
    {
        currentModel = Instantiate(GunModels[20]);
        currentModel.transform.position = new Vector3(0.1f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4f;
        single = false;
        collectionPanel.SetActive(false);
        r = 15;
        amo = r;
    }
    public void G21()
    {
        currentModel = Instantiate(GunModels[21]);
        currentModel.transform.position = new Vector3(-0.05f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 8f;
        single = true;
        collectionPanel.SetActive(false);
        r = 10;
        amo = r;
    }
    public void G22()
    {
        currentModel = Instantiate(GunModels[22]);
        currentModel.transform.position = new Vector3(-0.05f, 1.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 5f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G23()
    {
        currentModel = Instantiate(GunModels[23]);
        currentModel.transform.position = new Vector3(0.5f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 0.03f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G24()
    {
        currentModel = Instantiate(GunModels[24]);
        currentModel.transform.position = new Vector3(0.35f, -0.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 0.4f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G25()
    {
        currentModel = Instantiate(GunModels[25]);
        currentModel.transform.position = new Vector3(-0.2f, 0f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 0.6f;
        single = false;
        collectionPanel.SetActive(false);
        r = 30;
        amo = r;
    }
    public void G26()
    {
        currentModel = Instantiate(GunModels[26]);
        currentModel.transform.position = new Vector3(-0.2f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 11f;
        single = true;
        collectionPanel.SetActive(false);
        r = 8;
        amo = r;
    }
    public void G27()
    {
        currentModel = Instantiate(GunModels[27]);
        currentModel.transform.position = new Vector3(0.1f, -1.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 5f;
        single = true;
        collectionPanel.SetActive(false);
        r = 6;
        amo = r;
    }
    public void G28()
    {
        currentModel = Instantiate(GunModels[28]);
        currentModel.transform.position = new Vector3(-0.15f, -1f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 4f;
        single = true;
        collectionPanel.SetActive(false);
        r = 6;
        amo = r;
    }
    public void G29()
    {
        currentModel = Instantiate(GunModels[29]);
        currentModel.transform.position = new Vector3(-0.2f, -1.5f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 2.5f;
        single = true;
        collectionPanel.SetActive(false);
        r = 6;
        amo = r;
    }
}
