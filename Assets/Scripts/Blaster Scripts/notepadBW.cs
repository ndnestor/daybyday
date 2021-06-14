using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class notepadBW : MonoBehaviour
{
    //public OpenNotepadCombined openButton;
    // Start is called before the first frame update
    public SpriteRenderer rend;
    void Start()
    {
        //rend.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "noteColor")
        {
            //Destroy(gameObject);
        }
    }
}
