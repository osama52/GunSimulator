using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField] float Time;
    void Start()
    {
        Destroy(this.gameObject,Time);
    }

}
