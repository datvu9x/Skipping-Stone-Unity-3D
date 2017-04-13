using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged
{
    [SerializeField]
    Transform slotsMobile;
    [SerializeField]
    Transform slotsPC;
    [SerializeField]
    Transform slotServerMobile;
    [SerializeField]
    Transform slotServerPC;
    [SerializeField]
    Text inventoryText;
    [SerializeField]
    Text deviceText;

    // Use this for initialization
    void Start()
    {
        HasChanged();
    }

    #region IHasChanged implementation
    public void HasChanged()
    {
        inventoryText.text = "";
        System.Text.StringBuilder builder = new System.Text.StringBuilder();

        builder.Append(deviceText.text);

        foreach (Transform slotTransform in slotServerMobile)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                builder.Append("@" + slotTransform.name);
            }
        }

        foreach (Transform slotTransform in slotServerPC)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                builder.Append("@" + slotTransform.name);
            }
        }

        inventoryText.text = builder.ToString();
    }
    #endregion
}


namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}