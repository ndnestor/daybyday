using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fakeHealthBar_Plat : MonoBehaviour
{
    public Rigidbody2D bar;
    public float maxHealth;
    private float scaleChange;
    public float xSize_plat;

    public void healthBarSet()
    {
        scaleChange = xSize_plat * 0.1f;
        //Debug.Log("scaleChange is " + scaleChange);
        bar.transform.localScale = new Vector3(
            transform.localScale.x - scaleChange,
            transform.localScale.y,
            transform.localScale.z);
        //bar.transform.localScale.x = scaleChange;
    }
}
