using System;
using System.Collections;
using System.Linq;
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
        private const int BAR_SPRITE_SORTING_ORDER = 11;
        private bool isChronological;

        private readonly string[] interactions = { "Bookshelf", "Bonsai Tree", "Piano", "Yoga Mat", "Computer", "Window", "Other" };
        // TODO: Use dictionary instead?
        private readonly Hashtable todaysActivityTimes = new Hashtable();

        public static ProfileScreen Instance { get; private set; }

    
        private void Start() {
            Instance = this;
        
            barHeights = new float[Tracking.MAX_DAYS];

            ResetTodaysActivityTimes();
            
            var interactionNames = PersistentDataSaver.Instance
                .Get<string>($"Day{Tracking.Instance.DayNum}InteractionNames")
                .Split(',');
            var interactionTimes = PersistentDataSaver.Instance
                .Get<string>($"Day{Tracking.Instance.DayNum}InteractionTimes")
                .Split(',')
                .Select(int.Parse).ToArray();

            for(var i = 0; i < interactionNames.Length; i++)
                UpdateBarChart(interactionNames[i], interactionTimes[i]);
        }

        private void ToggleBarChartMode() {
            isChronological = !isChronological;
        
            chronologicalBars.SetActive(isChronological);
            sameOrderBars.SetActive(!isChronological);
        }
    
        private void UpdateBarChart(string interaction, int timeUsed) {
            UpdateBarChartChronological(interaction, timeUsed);
            UpdateBarChartSameOrder(interaction, timeUsed);
        }
        
        private void UpdateBarChartChronological(string interaction, int timeUsed) {
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

        private void UpdateBarChartSameOrder(string interaction, int timeUsed) {
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
            foreach(string currInteraction in interactions) {

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
            foreach(string currInteraction in interactions) {
                todaysActivityTimes[currInteraction] = 0;
            }
        }

        private Color GetInteractionColor(string interaction) {
            switch(interaction) {
                case "Bookshelf":
                    return bookshelfColor;
                case "Bonsai Tree":
                    return plantColor;
                case "Piano":
                    return pianoColor;
                case "Yoga Mat":
                    return yogaMatColor;
                case "Computer":
                    return computerColor;
                case "Window":
                    return windowColor;
                case "Other":
                    return otherColor;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
