using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryView : MonoBehaviour
{
    //4 item slots (if you somehow acquire all 4 at once)
    //5th slot is to show healing icon, only power-up that is not timed
    [SerializeField] private GameObject slot1, slot2, slot3, slot4; //, heal5;
    SpriteRenderer sprite1, sprite2, sprite3, sprite4;
    [SerializeField] private Text name1, name2, name3, name4;
    [SerializeField] private int despawnSpread, despawnRapid, despawnSwift, despawnBulwark;
    public Sprite heal, spread, rapid, swift, bulwark;
    Sprite addedSprite;
    float timer1, timer2, timer3, timer4;

    public void Start() {
        //Set all icons/timers to inactive/empty at start
        /**sprite1 = slot1.GetComponent<SpriteRenderer>();
        sprite2 = slot2.GetComponent<SpriteRenderer>();
        sprite3 = slot3.GetComponent<SpriteRenderer>();
        sprite4 = slot4.GetComponent<SpriteRenderer>();*/
        slot1.SetActive(false);
        slot2.SetActive(false);
        slot3.SetActive(false);
        slot4.SetActive(false);
        name1.text = "";
        name2.text = "";
        name3.text = "";
        name4.text = "";

    }
    public void addToInventory(string addedItem) {
        //Call from PlayerMovement script
        switch(addedItem) {
            case "Heal":
                break;
            case "Spread":
                addedSprite = spread;
                break;
            case "Rapid":
                addedSprite = rapid;
                break;
            case "Swift":
                addedSprite = swift;
                break;
            case "Bulwark":
                addedSprite = bulwark;
                break;
            default:
                Debug.Log("No known object picked up");
                break;
        }
        assignSlot();
    }

    public void assignSlot() {
        if (!slot1.activeSelf) {
            slot1.GetComponent<SpriteRenderer>().sprite = addedSprite;
            slot1.SetActive(true);
        } else if (!slot2.activeSelf) {
            slot2.GetComponent<SpriteRenderer>().sprite = addedSprite;
            slot2.SetActive(true);
        } else if (!slot3.activeSelf) {
            slot3.GetComponent<SpriteRenderer>().sprite = addedSprite;
            slot3.SetActive(true);
        } else if (!slot4.activeSelf) {
            slot4.GetComponent<SpriteRenderer>().sprite = addedSprite;
            slot4.SetActive(true);
        }
    }
}
