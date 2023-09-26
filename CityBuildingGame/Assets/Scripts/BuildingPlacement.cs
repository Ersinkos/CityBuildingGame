using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlaced;
    private bool currentlyDestroyed; //with bulldozer
    private BuildingPreset currentBuildingPreset;

    private float indicatorUpdateTime = 0.05f;
    private float lastUpdateTime;
    private Vector3 currentIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;
    public LayerMask road;
    public bool isRoad = false;
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        //if(City.instance.money<preset.cost)
        //{
        //    return;
        //}
        currentlyPlaced = true;
        currentBuildingPreset = preset;
        placementIndicator.SetActive(true);
    }
    void CancelBuildingPlacement()
    {
        currentlyPlaced = false;
        placementIndicator.SetActive(false);
    }
    public void ToggleBulldozer()
    {
        currentlyDestroyed = !currentlyDestroyed;
        bulldozerIndicator.SetActive(currentlyDestroyed);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuildingPlacement();
            isRoad = false;
        }
        if (Time.time - lastUpdateTime > indicatorUpdateTime)
        {
            lastUpdateTime = Time.time;
            currentIndicatorPos = Selector.instance.GetCurrentTilePosition();
            if (currentlyPlaced)
            {
                placementIndicator.transform.position = currentIndicatorPos;
            }
            else if (currentlyDestroyed)
            {
                bulldozerIndicator.transform.position = currentIndicatorPos;
            }
        }
        if (Input.GetMouseButtonDown(0) && currentlyPlaced && RoadCheck())
        {
            PlaceBuilding();
        }
        else if (Input.GetMouseButtonUp(0) && currentlyDestroyed)
        {
            DestroyBuilding();
        }
    }
    void PlaceBuilding()
    {
        GameObject buildingObject = Instantiate(currentBuildingPreset.prefab, currentIndicatorPos, Quaternion.Euler(RotateIndicator.instance.buildingRotation.x, RotateIndicator.instance.buildingRotation.y, RotateIndicator.instance.buildingRotation.z));
        City.instance.OnPlaceBuilding(buildingObject.GetComponent<Building>());
        CancelBuildingPlacement();
    }

    public void HouseChosed()
    {
        placementIndicator.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        placementIndicator.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        placementIndicator.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        isRoad = false;
    }
    public void FactoryChosed()
    {
        placementIndicator.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        placementIndicator.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        placementIndicator.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        isRoad = false;
    }
    public void FarmChosed()
    {
        placementIndicator.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        placementIndicator.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        placementIndicator.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        isRoad = false;
    }
    public void RoadChosed()
    {
        isRoad = true;
    }
    void DestroyBuilding()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == currentIndicatorPos);
        if (buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
    public bool RoadCheck()
    {
        if (isRoad) return true;
        else return Physics.CheckSphere(placementIndicator.transform.position, 1.0f, road);
    }
}
