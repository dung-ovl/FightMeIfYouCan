using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfile", menuName = "ScriptableObject/ItemProfile")]
public class ItemProfileSO : ScriptableObject
{
    public ItemCode itemCode = ItemCode.NoItem;
    public string itemName = "no-name";

}