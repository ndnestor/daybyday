using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Computer {
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

        [SerializeField] private GameObject chronologicalBars;
        [SerializeField] private GameObject sameOrderBars;

        private float[] barHeights;
        private const int BAR_SPRITE_SORTING_ORDER = 1;
        private bool isChronological;

        private readonly Hashtable todaysActivityTimes = new Hashtable();

        public static ProfileScreen Instance { get; private set; }
    
        private enum Interaction {
            Plant,
            Piano,
            YogaMat,
            Computer,
            Bookshelf,
            Window,
            Other
        }
    
        private void Start() {
            Instance = this;
        
            barHeights = new float[Tracking.MAX_DAYS];

            ResetTodaysActivityTimes();
        }

        private void ToggleBarChartMode() {
            isChronological = !isChronological;
        
            chronologicalBars.SetActive(isChronological);
            sameOrderBars.SetActive(!isChronological);
        
            // TODO: Determine if above code works. If not, use the below code but modify as it does not work currently
            /*foreach(SpriteRenderer spriteRenderer in chronologicalBarRenderers) {
            spriteRenderer.enabled = isChronological;
            }
            foreach(SpriteRenderer spriteRenderer in sameOrderBarRenderers) {
                spriteRenderer.enabled = !isChronological;
            }*/
        }
    
        private void UpdateBarChart(Interaction interaction, int timeUsed) {
        
            // Update the chronological bar chart
            UpdateBarChartChronological(interaction, timeUsed);
        
            // Update the same order bar chart
            UpdateBarChartSameOrder(interaction, timeUsed);
        }
        
        private void UpdateBarChartChronological(Interaction interaction, int timeUsed) {
            // Create the new bar chart section
            GameObject newBarChartSection = Instantiate(barChartSectionPrefab, barChartOrigin.position, Quaternion.identity,
                chronologicalBars.transform);
            newBarChartSection.name = "Chronological Bar Chart Section";
        
            // Set its sprite renderer properties
            SpriteRenderer newSpriteRenderer = newBarChartSection.GetComponent<SpriteRenderer>();
            if(!isChronological) {
                newSpriteRenderer.enabled = false;
            }
            newSpriteRenderer.sortingOrder = BAR_SPRITE_SORTING_ORDER;

            // Set its dimensions
            float barHeight = timeUsed * timeUnitHeight;
            newBarChartSection.transform.localScale = new Vector3(barWidth, barHeight, 1);
        
            // Set its location
            int dayNum = Tracking.Instance.DayNum;
            float xPos = (barPadding + barWidth) * (dayNum - 1);
            float yPos = barHeights[dayNum - 1] + 0.5f * barHeight;
            newBarChartSection.transform.localPosition = new Vector3(xPos, yPos, 0);
        
            // Set bar heights
            barHeights[dayNum - 1] = yPos + 0.5f * barHeight;
        
            // Set its color
            newBarChartSection.GetComponent<SpriteRenderer>().color = GetInteractionColor(interaction);
        }

        private void UpdateBarChartSameOrder(Interaction interaction, int timeUsed) {
            todaysActivityTimes[interaction] = (int)todaysActivityTimes[interaction] + timeUsed;

            // Clear bar chart
            Transform sameOrderBarTransform = sameOrderBars.transform;

            Transform currDaySection;
            if(sameOrderBarTransform.childCount < Tracking.Instance.DayNum) {
                currDaySection = new GameObject().transform;
                currDaySection.parent = sameOrderBarTransform;
                currDaySection.localPosition = Vector3.zero;
                currDaySection.name = "Day " + Tracking.Instance.DayNum;
            } else {
                currDaySection = sameOrderBarTransform.GetChild(Tracking.Instance.DayNum - 1);
            }

            if(currDaySection.childCount >= Tracking.Instance.DayNum) {
                for(int i = 0; i < currDaySection.childCount; i++) {
                    Destroy(currDaySection.GetChild(i).gameObject);
                }
            }
            
            // Loop through every interaction type
            float totalBarHeight = 0;
            foreach(Interaction currInteraction in Enum.GetValues(typeof(Interaction))) {

                // Create the new bar chart section
                GameObject newBarChartSection = Instantiate(barChartSectionPrefab, barChartOrigin.position, Quaternion.identity,
                    currDaySection);
                newBarChartSection.name = "Same Order Bar Chart Section";
            
                // Set its sprite renderer properties
                SpriteRenderer newSpriteRenderer = newBarChartSection.GetComponent<SpriteRenderer>();
                if(isChronological) {
                    newSpriteRenderer.enabled = false;
                }
                newSpriteRenderer.sortingOrder = BAR_SPRITE_SORTING_ORDER;

                // Set its dimensions
                float barHeight = (int)todaysActivityTimes[currInteraction] * timeUnitHeight;
                newBarChartSection.transform.localScale = new Vector3(barWidth, barHeight, 1);
        
                // Set its location
                int dayNum = Tracking.Instance.DayNum;
                float xPos = (barPadding + barWidth) * (dayNum - 1);
                float yPos = totalBarHeight + 0.5f * barHeight;
                newBarChartSection.transform.localPosition = new Vector3(xPos, yPos, 0);
            
                newSpriteRenderer.color = GetInteractionColor(currInteraction);

                totalBarHeight += barHeight;
            }
        }

        public void ResetTodaysActivityTimes() {
            foreach(Interaction currInteraction in Enum.GetValues(typeof(Interaction))) {
                todaysActivityTimes[currInteraction] = 0;
            }
        }

        private Color GetInteractionColor(Interaction interaction) {
            switch(interaction) {
                case Interaction.Bookshelf:
                    return bookshelfColor;
                case Interaction.Plant:
                    return plantColor;
                case Interaction.Piano:
                    return pianoColor;
                case Interaction.YogaMat:
                    return yogaMatColor;
                case Interaction.Computer:
                    return computerColor;
                case Interaction.Window:
                    return windowColor;
                case Interaction.Other:
                    return otherColor;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.B)) {
                string[] activityNames = {"plant", "piano", "yoga mat", "computer", "bookshelf", "window", "other"};
                int randomIndex = Random.Range(0, activityNames.Length);
                UpdateBarChart((Interaction)Enum.GetValues(typeof(Interaction)).GetValue(randomIndex), Random.Range(1, 4));
            }
        }
    }
}
