using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductivityAid : MonoBehaviour {

    [SerializeField] private int[] levelIncrementDays;
    
    private int level = 0;

    // Called at the start of each day
    public void UpdateLevel() {
        if(levelIncrementDays.Contains(Tracking.Instance.DayNum)) {
            level++;
        }
    }
}
