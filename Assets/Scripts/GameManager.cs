using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject currentModel;
    public  List<GameObject> GunModels;
    public GameObject collectionPanel;
    public GameObject gunDisplayPanel;
    public GameObject settingPanel;
    public GameObject Content;
    public GameObject ClassicContent;
    public GameObject SciFiContent;
    public GameObject TaserContent;
    public GameObject ReloadBtn;
    public GameObject StartPanel;
    public GameObject ClassicCollection;
    public GameObject SciFiCollection;
    public GameObject TeaserCollection;
    public Slider energy;
    //public GameObject BG;
    public Text amoText;
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

    float amo = 10;
    float r;

    bool sound = true;
    bool vibrate = true;
    bool flash = true;


    int reloadCount;

    public void OpenClassic()
    {
        StartPanel.SetActive(false);
        ClassicCollection.SetActive(true);
        SciFiCollection.SetActive(false);
        TeaserCollection.SetActive(false);
        Content = ClassicContent;
    }
    public void OpenSciFi()
    {
        StartPanel.SetActive(false);
        ClassicCollection.SetActive(false);
        SciFiCollection.SetActive(true);
        TeaserCollection.SetActive(false);
        Content= SciFiContent;
    }
    public void OpenTeaser()
    {
        StartPanel.SetActive(false);
        ClassicCollection.SetActive(false);
        SciFiCollection.SetActive(false);
        TeaserCollection.SetActive(true);
        Content = TaserContent;
    }
    public void Home()
    {
        StartPanel.SetActive(true);
    }
    public void Sound()
    {
        sound=!sound;
        if(sound)
        {
            PlayerPrefs.SetInt("Sound", 0);
            settingPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            settingPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void Vibrate()
    {
        vibrate = !vibrate;
        if (vibrate)
        {
            PlayerPrefs.SetInt("Vibrate", 0);
            settingPanel.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            settingPanel.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void Flash()
    {
        flash = !flash;
        if (flash)
        {
            PlayerPrefs.SetInt("Flash", 0);
            settingPanel.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Flash", 1);
            settingPanel.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void Awake()
    {
        instance= this;
    }
    public AndroidJavaClass javaObject;

    void Start()
    {
        javaObject = new AndroidJavaClass("com.myflashlight.flashlightlib.Flashlight");


        if(PlayerPrefs.GetInt("Sound", 0)==0)
        {
            settingPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            sound= true;
        }
        else
        {
            settingPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            sound= false;
        }

        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
        {
            settingPanel.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            vibrate=true;
        }
        else
        {
            settingPanel.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
            vibrate=false;
        }

        if (PlayerPrefs.GetInt("Flash", 0) == 0)
        {
            settingPanel.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            flash=true;
        }
        else
        {
            settingPanel.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            flash=false;
        }

        if (PlayerPrefs.GetInt("G5", 0) == 1)
        {
            SciFiContent.transform.GetChild(5).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G6", 0) == 1)
        {
            SciFiContent.transform.GetChild(6).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G12", 0) == 1)
        {
            ClassicContent.transform.GetChild(12).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G14", 0) == 1)
        {
            ClassicContent.transform.GetChild(14).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G17", 0) == 1)
        {
            ClassicContent.transform.GetChild(17).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G23", 0) == 1)
        {
            ClassicContent.transform.GetChild(23).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G27", 0) == 1)
        {
            ClassicContent.transform.GetChild(27).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G29", 0) == 1)
        {
            ClassicContent.transform.GetChild(29).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G31", 0) == 1)
        {
            TaserContent.transform.GetChild(31).GetChild(3).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("G36", 0) == 1)
        {
            TaserContent.transform.GetChild(36).GetChild(3).gameObject.SetActive(false);
        }
    }

    public void TurnOn()
    {
        javaObject.CallStatic("on", GetUnityActivity());
    }

    public void TurnOff()
    {
        javaObject.CallStatic("off", GetUnityActivity());
    }
    AndroidJavaObject GetUnityActivity()
    {
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
    private IEnumerator BlinkTorch()
    {
        //BG.SetActive(true);
        TurnOn();
        yield return new WaitForSeconds(0.1f);
        //BG.SetActive(false);
        TurnOff();
    }

    public void openSettings()
    {
        settingPanel.SetActive(true);
    }
    public void closeSettings()
    {
        settingPanel.SetActive(false);
    }
    private void Update()
    {
        energy.value = amo / r;

        if(amo<=0)
        {
            ReloadBtn.SetActive(true);
        }
        else
        {
            ReloadBtn.SetActive(false);
        }
        amoText.text = amo.ToString();
        if (Hold.isOn)
        {
            if(isHold )
            {
                
                if(single)
                {
                   
                    if (Time.time > coolTime && amo > 0)
                    {
                        if (PlayerPrefs.GetInt("Sound", 0) == 0)
                        {
                            currentModel.GetComponent<AudioSource>().Play();
                        }
                        if (PlayerPrefs.GetInt("Vibrate",0)==0)
                        {
                            Vibration.Vibrate(200);
                        }
                        if (PlayerPrefs.GetInt("Flash", 0) == 0)
                        {
                            StartCoroutine(BlinkTorch());
                        }                                              
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        currentModel.GetComponent<Animator>().Play("Shoot");
                        coolTime = Time.time + 0.5f;
                        amo--;

                    }
                }
                else
                {
                    //BG.SetActive(true);
                    //Auto
                    if (Time.time > coolTime && amo > 0)
                    {
                        if (PlayerPrefs.GetInt("Sound", 0) == 0)
                        {
                            currentModel.GetComponent<AudioSource>().Play();
                        }
                        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
                        {
                            Vibration.Vibrate(200);
                        }
                        if (PlayerPrefs.GetInt("Flash", 0) == 0)
                        {
                            StartCoroutine(BlinkTorch());
                        }
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        currentModel.GetComponent<Animator>().Play("AutoShoot");
                        coolTime = Time.time + 0.15f;
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
                        if (PlayerPrefs.GetInt("Sound", 0) == 0)
                        {
                            currentModel.GetComponent<AudioSource>().Play();
                        }
                        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
                        {
                            Vibration.Vibrate(200);
                        }
                        if (PlayerPrefs.GetInt("Flash", 0) == 0)
                        {
                            StartCoroutine(BlinkTorch());
                        }
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                        currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
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
    public void SingleFire()
    {
      
        if(Single.isOn)
        {
            if(Time.time > coolTime && amo>0)
            {
                if (PlayerPrefs.GetInt("Sound", 0) == 0)
                {
                    currentModel.GetComponent<AudioSource>().Play();
                }
                if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
                {
                    Vibration.Vibrate(200);
                }
                if (PlayerPrefs.GetInt("Flash", 0) == 0)
                {
                    StartCoroutine(BlinkTorch());
                }
                currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                currentModel.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                currentModel.GetComponent<Animator>().Play("Shoot");
                currentModel.GetComponent<Animator>().Play("Shoot");
                coolTime=Time.time+0.5f;
                amo--;
            }
           
        }
       
    }
    public void OpenCollectionPanel()
    {
        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(true);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(true);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(true);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(true);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(true);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(false);
        collectionPanel.SetActive(true);
        Destroy(currentModel);
    }
    
    public void HideUI()
    {
        if(Hide.isOn)
        {
            gunDisplayPanel.transform.GetChild(2).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(6).gameObject.SetActive(false);
        }
        else
        {
            gunDisplayPanel.transform.GetChild(2).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(true);
            gunDisplayPanel.transform.GetChild(6).gameObject.SetActive(true);
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
        //BG.SetActive(false);
        isHold = false;
    }
    public void Reload()
    {
        if(amo<=0)
        {
            // if (reloadCount % 3 == 0)
            // {
            if (AdManager_Admob.instance.IsInterstitialAdLoaded())
            {
                AdManager_Admob.instance.ShowInterstitialAd();
                amo = r;
                currentModel.GetComponent<Animator>().Play("Reload");
                reloadCount++;
            }
            else
            {
                amo = r;
                currentModel.GetComponent<Animator>().Play("Reload");
                reloadCount++;
            }
          //  }
           
           
        }   
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
        if(PlayerPrefs.GetInt("G5",0)==1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd (() =>
            {
                Content.transform.GetChild(5).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G5", 1);
            });
           
        }
    }
    public void G6()
    {
        if (PlayerPrefs.GetInt("G6", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(6).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G6", 1);
            });

        }
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
        if (PlayerPrefs.GetInt("G12", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(12).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G12", 1);
            });
        }
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
        if (PlayerPrefs.GetInt("G14", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(14).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G14", 1);
            });
        }
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
        if (PlayerPrefs.GetInt("G17", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(17).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G17", 1);
            });
        }
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
        if (PlayerPrefs.GetInt("G23", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(23).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G23", 1);
            });
        }
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
        if (PlayerPrefs.GetInt("G27", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(27).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G27", 1);
            });
        }
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
        if (PlayerPrefs.GetInt("G29", 0) == 1)
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
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(29).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G29", 1);
            });
        }
    }
    public void G30()
    {
        currentModel = Instantiate(GunModels[30]);
        currentModel.transform.position = new Vector3(0f, -0.3f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 8f;
        single = false;
        collectionPanel.SetActive(false);
        r = 25;
        amo = r;
        Hold.isOn = true;

        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
    }
    public void G31()
    {
        if (PlayerPrefs.GetInt("G31", 0) == 1)
        {
            currentModel = Instantiate(GunModels[31]);
            currentModel.transform.position = new Vector3(-1f, 0f, 0f);
            currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
            currentModel.transform.localScale = Vector3.one * 1f;
            single = false;
            collectionPanel.SetActive(false);
            r = 25;
            amo = r;
            Hold.isOn = true;

            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
        }
        else
        {
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(31).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G31", 1);
            });
        }
    }
    public void G32()
    {
        currentModel = Instantiate(GunModels[32]);
        currentModel.transform.position = new Vector3(-0.1f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1f;
        single = false;
        collectionPanel.SetActive(false);
        r = 25;
        amo = r;
        Hold.isOn = true;

        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);

    }
    public void G33()
    {
        currentModel = Instantiate(GunModels[33]);
        currentModel.transform.position = new Vector3(-0.1f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1f;
        single = false;
        collectionPanel.SetActive(false);
        r = 25;
        amo = r;
        Hold.isOn = true;

        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
    }
    public void G34()
    {
        currentModel = Instantiate(GunModels[34]);
        currentModel.transform.position = new Vector3(-0.1f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1f;
        single = false;
        collectionPanel.SetActive(false);
        r = 25;
        amo = r;
        Hold.isOn = true;

        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
    }
    public void G35()
    {
        currentModel = Instantiate(GunModels[35]);
        currentModel.transform.position = new Vector3(0f, 0.2f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1f;
        single = false;
        collectionPanel.SetActive(false);
        r = 25;
        amo = r;
        Hold.isOn = true;

        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
    }

    public void G36()
    {
        if (PlayerPrefs.GetInt("G36", 0) == 1)
        {
            currentModel = Instantiate(GunModels[36]);
            currentModel.transform.position = new Vector3(0f, -0.45f, 0f);
            currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
            currentModel.transform.localScale = Vector3.one * 1f;
            single = false;
            collectionPanel.SetActive(false);
            r = 25;
            amo = r;
            Hold.isOn = true;

            gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
            gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
        }
        else
        {
            print(Content.transform.GetChild(36).GetChild(3).name);
            AdManager_Admob.instance.ShowRewardedVideoAd(() =>
            {
                Content.transform.GetChild(36).GetChild(3).gameObject.SetActive(false);
                PlayerPrefs.SetInt("G36", 1);
            });
        }
    }
    public void G37()
    {

        currentModel = Instantiate(GunModels[37]);
        currentModel.transform.position = new Vector3(0f, 0.4f, 0f);
        currentModel.transform.rotation = Quaternion.Euler(0, 180, 90);
        currentModel.transform.localScale = Vector3.one * 1f;
        single = false;
        collectionPanel.SetActive(false);
        r = 25;
        amo = r;
        Hold.isOn = true;

        gunDisplayPanel.transform.GetChild(3).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(4).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(5).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(7).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(8).gameObject.SetActive(false);
        gunDisplayPanel.transform.GetChild(10).gameObject.SetActive(true);
    }
}
