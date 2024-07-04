using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]
    private PlatesCounter platesCounter;
    [SerializeField]
    private Transform counterTopPoint;
    [SerializeField]
    private Transform plateVisualPrefab;
    [SerializeField]
    private float plateOffsetY = 0.1f;

    private List<GameObject> plateVisuals;

    private void Awake()
    {
        plateVisuals = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisuals[plateVisuals.Count - 1];
        plateVisuals.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        
        plateVisualTransform.localPosition = 
            new Vector3(0f, plateOffsetY * plateVisuals.Count, 0f);
        plateVisuals.Add(plateVisualTransform.gameObject);
    }
}
