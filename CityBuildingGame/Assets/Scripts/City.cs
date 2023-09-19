using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{
    public int money;
    public int day;
    public int currentPopulation;
    public int currentJobs;
    public int currentFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJob;

    public TextMeshProUGUI statsText;
    public List<Building> buildings = new List<Building>();

    public static City instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateStatText();
    }
    public void OnPlaceBuilding(Building building)
    {
        money -= building.preset.cost;
        maxPopulation += building.preset.population;
        maxJobs += building.preset.jobs;
        buildings.Add(building);
        UpdateStatText();
    }
    public void OnRemoveBuilding(Building building)
    {
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText();
    }
    void UpdateStatText()
    {
        statsText.text = string.Format("Day: {0} Money: {1} Population: {2} / {3} Jobs: {4} / {5} Food: {6}", new object[7] { day, money, currentPopulation, maxPopulation, currentJobs, maxJobs, currentFood });
    }
    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();

        UpdateStatText();
    }
    void CalculateMoney()
    {
        money += currentJobs * incomePerJob;
        foreach (Building building in buildings)
        {
            money -= building.preset.costPerTurn;
        }
    }
    void CalculatePopulation()
    {
        if (currentFood >= currentPopulation && currentPopulation < maxPopulation)
        {
            currentFood -= currentPopulation / 4;
            currentPopulation = Mathf.Min(currentPopulation + (currentFood / 4), maxPopulation);
        }
        else if (currentFood < currentPopulation)
        {
            currentPopulation = currentFood;
        }
    }
    void CalculateJobs()
    {
        currentJobs = Mathf.Min(currentPopulation, maxJobs);
    }
    void CalculateFood()
    {
        currentFood = 0;
        foreach (Building building in buildings)
        {
            currentFood += building.preset.food;
        }
    }
}
