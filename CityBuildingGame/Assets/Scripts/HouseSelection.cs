using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HouseSelection : MonoBehaviour
{
    public GameObject HouseInfoUI;
    public TextMeshProUGUI populationText;
    public BuildingPreset housePreset;
    public BuildingPreset apartmentPreset;
    public float offset = 1.5f;

    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    // Use this for initialization
    void Start()
    {
        definedButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
                Debug.Log("Button Clicked");
                OnClick.Invoke();
            }
        }
    }
    public void Upgrade()
    {
        Vector3 upgradePosition = transform.position;
        upgradePosition.y += offset;
        GameObject buildingObject = (GameObject)Instantiate(apartmentPreset.prefab, upgradePosition, Quaternion.identity);
        City.instance.OnRemoveBuilding(gameObject.GetComponent<Building>());
        City.instance.OnPlaceBuilding(buildingObject.GetComponent<Building>());
        Debug.Log("House Upgraded");

    }
    public void HouseSelected()
    {
        populationText.text = "Population:" + housePreset.cost.ToString();
        HouseInfoUI.SetActive(true);
        Debug.Log("House Selected");
    }
    public void CloseHouseInfoUI()
    {
        HouseInfoUI.SetActive(false);
    }
}
