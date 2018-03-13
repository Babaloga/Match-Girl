﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnAwake : MonoBehaviour {

    public GameObject target;

    private void Awake()
    {
        target.SetActive(true);
    }
}