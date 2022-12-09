using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColor : MonoBehaviour
{
    private int week, level, month;
    public SpriteRenderer treeSprite;

    // Sprites relevant to Week 1 Level 1
    public Sprite w1l1;
    // Sprites relevant to Week 2 Level 1
    public Sprite w2l1;
    // Sprites relevant to Week 2 Level 2
    public Sprite w2l2_JanFebNovDec;
    public Sprite w2l2_Mar, w2l2_Apr, w2l2_May, w2l2_Jun, w2l2_Jul, w2l2_Aug, w2l2_Sep, w2l2_Oct;
    // Sprites relevant to Week 3 Level 1
    public Sprite w3l1_JanFebMarOctNovDec, w3l1_JunJulAug;
    public Sprite w3l1_Sep;
    //Sprites relevant to Week 3 Level 2
    public Sprite w3l2_JanFebNovDec, w3l2_JunJulyAug, w3l2_Sep, w3l2_Oct;
    // Sprites relevant to Week 3 Level 3
    public Sprite w3l3_JanFebNov, w3l3_Mar, w3l3_May, w3l3_Jun, w3l3_Jul, w3l3_Aug, w3l3_Sep, w3l3_Oct, w3l3_Dec;
    // Sprites reused across Week 3 Level 1-3
    public Sprite w3l1_Apr, w3l1_May, w3l2_May;

    public WaterPlant waterPlant;

    private void Update()
    {
        level = waterPlant.level;
        week = waterPlant.day;
        if (week == 1) {
            treeSprite.sprite = w1l1;
        }
        if (week == 2) {
            if (level == 1) {
                treeSprite.sprite = w2l1;
            }
            if (level == 2) {
                if (month == 3) {
                    treeSprite.sprite = w2l2_Mar;
                }
                else if (month == 4) {
                    treeSprite.sprite = w2l2_Apr;
                }
                else if (month == 5) {
                    treeSprite.sprite = w2l2_May;
                }
                else if (month == 6) {
                    treeSprite.sprite = w2l2_Jun;
                }
                else if (month == 7) {
                    treeSprite.sprite = w2l2_Jul;
                }
                else if (month == 8) {
                    treeSprite.sprite = w2l2_Aug;
                }
                else if (month == 9) {
                    treeSprite.sprite = w2l2_Sep;
                }
                else if (month == 10) {
                    treeSprite.sprite = w2l2_Oct;
                }
                else {
                    treeSprite.sprite = w2l2_JanFebNovDec;
                }
            }
        }
        if (week == 3) {
            if (level == 1) {
                if (month == 4) {
                    treeSprite.sprite = w3l1_Apr;
                }
                else if (month == 5) {
                    treeSprite.sprite = w3l1_May;
                }
                else if (month == 6 || month == 7 || month == 8) {
                    treeSprite.sprite = w3l1_JunJulAug;
                }
                else if (month == 9) {
                    treeSprite.sprite = w3l1_Sep;
                }
                else {
                    treeSprite.sprite = w3l1_JanFebMarOctNovDec;
                }
            }
            if (level == 2) {
                if (month == 3) {
                    treeSprite.sprite = w3l1_Apr;
                }
                else if (month == 4) {
                    treeSprite.sprite = w3l1_May;
                }
                else if (month == 5) {
                    treeSprite.sprite = w3l2_May;
                }
                else if (month == 6 || month == 7 || month == 8) {
                    treeSprite.sprite = w3l2_JunJulyAug;
                }
                else if (month == 9) {
                    treeSprite.sprite = w3l2_Sep;
                }
                else if (month == 10) {
                    treeSprite.sprite = w3l2_Oct;
                }
                else if (month == 1 || month == 2 || month == 11 || month == 12) {
                    treeSprite.sprite = w3l2_JanFebNovDec;
                }
            }
            if (level == 3) {
                if (month == 1 || month == 2 || month == 11) {
                    treeSprite.sprite = w3l3_JanFebNov;
                }
                else if (month == 3) {
                    treeSprite.sprite = w3l3_Mar;
                }
                else if (month == 4) {
                    treeSprite.sprite = w3l2_May;
                }
                else if (month == 5) {
                    treeSprite.sprite = w3l3_May;
                }
                else if (month == 6) {
                    treeSprite.sprite = w3l3_Jun;
                }
                else if (month == 7) {
                    treeSprite.sprite = w3l3_Jul;
                }
                else if (month == 8) {
                    treeSprite.sprite = w3l3_Aug;
                }
                else if (month == 9) {
                    treeSprite.sprite = w3l3_Sep;
                }
                else if (month == 10) {
                    treeSprite.sprite = w3l3_Oct;
                }
                else if (month == 12) {
                    treeSprite.sprite = w3l3_Dec;
                }
            }
        }
    }
}
