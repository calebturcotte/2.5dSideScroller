﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position;        
    }
}
