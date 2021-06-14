using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fakeHealthBar : MonoBehaviour
{
    public Rigidbody2D bar;
    public float maxHealth;
    private float scaleChange;
    public float xSize;

    public void healthBarSet()
    {
        scaleChange = xSize * (0.2f);
        bar.transform.localScale = new Vector3(transform.localScale.x - scaleChange,
        transform.localScale.y,
        transform.localScale.z);
    }
    public void healthBarSetHeal()
    {
        scaleChange = xSize * (0.2f);
        bar.transform.localScale = new Vector3(transform.localScale.x + scaleChange/2,
        transform.localScale.y,
        transform.localScale.z);
    }
}
