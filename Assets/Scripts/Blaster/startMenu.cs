﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public void openMenu()
    {
        SceneLoader.Instance.UnloadAsync(gameObject.scene.name);
    }
}
