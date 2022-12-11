using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bonsai Tree State", menuName = "Bonsai Tree/Bonsai Tree State")]
public class BonsaiTreeState : ScriptableObject {
    public int woodLevel;
    public int leafLevel;
    public int earliestDay;
    public int latestDay;
    public Sprite sprite;
}
