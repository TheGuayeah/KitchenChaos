using UnityEngine;

[CreateAssetMenu(fileName = "New KitchenObjectSO", menuName = "SrictableObjects/KitchenObjectSO", order = 0)]
public class KitchenObjectSO : ScriptableObject
{
	public Transform prefab;
	public Sprite sprite;
	public string objectName;
}
