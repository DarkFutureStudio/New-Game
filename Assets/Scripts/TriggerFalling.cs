﻿using UnityEngine;
using System.Collections;

public class TriggerFalling : MonoBehaviour
{
    public BoxCollider2D[] m_boxColliders;
    public float m_wait = 2;
    private int i = 0;

    private ShakeScript shakeScript;

    void Start()
    {
        shakeScript = GameObject.FindGameObjectWithTag("ShakeManager").GetComponent<ShakeScript>();
    }

    //ivoke function call a function after specified time InvokeRepeating call it untill cacelInvoke call
    void OnTriggerEnter2D() => InvokeRepeating("ColliderDisabler", m_wait, m_wait);
    
    void ColliderDisabler()
    {
        shakeScript.shaker();
        //i++ when calculate shows previouse value so i will change after this statement...
        m_boxColliders[i++].enabled = false;
        //like Debug.Log(), why should we use that instead of print
        print($"{i} is disabled");

        if (i == m_boxColliders.Length)
        {
            //if calcelInvoke get argument it cancels exacly that functin invoke
            CancelInvoke("ColliderDisabler");
            Destroy(this.gameObject);
        }
    }
}
