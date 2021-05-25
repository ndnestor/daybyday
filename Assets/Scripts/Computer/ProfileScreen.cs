using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProfileScreen : MonoBehaviour {
    
    [SerializeField] private float timeUnitHeight;
    [SerializeField] private float barWidth;
    [SerializeField] private float barPadding;
    [SerializeField] private Transform barChartOrigin;
    [SerializeField] private GameObject barChartSectionPrefab;
    
    [SerializeField] private Color plantColor;
    [SerializeField] private Color pianoColor;
    [SerializeField] private Color yogaMatColor;
    [SerializeField] private Color computerColor;
    [SerializeField] private Color bookshelfColor;
    [SerializeField] private Color windowColor;
    [SerializeField] private Color otherColor;

    // TODO: Remove this magic number
    private float[] barHeights = new float[10];
    private const int BAR_CHART_SORTING_ORDER = 1;
    private bool isChronological;

    private void UpdateBarChart(string activityName, int timeUsed) {

        // Create the new bar chart section
        GameObject newBarChartSection = Instantiate(barChartSectionPrefab, barChartOrigin.position, Quaternion.identity,
            barChartOrigin);
        
        // Set its sorting order to be sorted in front of the computer
        newBarChartSection.GetComponent<SpriteRenderer>().sortingOrder = BAR_CHART_SORTING_ORDER;

        // Set its dimensions
        float barHeight = timeUsed * timeUnitHeight;
        newBarChartSection.transform.localScale = new Vector3(barWidth, barHeight, 1);
        
        // Set its location
        int dayNum = Tracking.Instance.dayNum;
        float xPos = (barPadding + barWidth + 0.5f) * (dayNum - 1);
        float yPos = barHeights[dayNum - 1] + 0.5f * barHeight;
        newBarChartSection.transform.localPosition = new Vector3(xPos, yPos, 0);
        
        // Set bar heights
        barHeights[dayNum - 1] = yPos + 0.5f * barHeight;
        
        // Set its color
        Color barChartSectionColor;
        switch(activityName) {
            case "piano":
                barChartSectionColor = pianoColor;
                break;
            case "yoga mat":
                barChartSectionColor = yogaMatColor;
                break;
            case "computer":
                barChartSectionColor = computerColor;
                break;
            case "bookshelf":
                barChartSectionColor = bookshelfColor;
                break;
            case "plant":
                barChartSectionColor = plantColor;
                break;
            case "window":
                barChartSectionColor = windowColor;
                break;
            case "other":
                barChartSectionColor = otherColor;
                break;
            default:
                Debug.LogError("Invalid activity name. Setting bar color to magenta");
                barChartSectionColor = Color.magenta;
                break;
        }
        newBarChartSection.GetComponent<SpriteRenderer>().color = barChartSectionColor;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.B)) {
            string[] activityNames = {"plant", "piano", "yoga mat", "computer", "bookshelf", "window", "other"};
            int randomIndex = (int)Random.Range(0, activityNames.Length - 1);
            UpdateBarChart(activityNames[randomIndex], Random.Range(1, 4));
        }
    }
}
