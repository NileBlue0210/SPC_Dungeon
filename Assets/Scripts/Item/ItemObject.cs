using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string itemInfo = $"{data.displayName}\n{data.description}";

        return itemInfo;
    }

    public void OnInteract()
    {
        if (data.type != ItemType.Object)
        {
            UnitManager.Instance.Player.itemData = data;
            UnitManager.Instance.Player.addItem?.Invoke();

            Destroy(gameObject);
        }
    }
}
