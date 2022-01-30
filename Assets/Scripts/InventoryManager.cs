using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private HUD hud;
    private int currentIndex = 0;
    private KeyCode currentKey;
    private bool dropItem = false;
    private bool putAwayItem = false;

    public InventorySlot[] slots;
    public GameObject holdHand;
    public Camera mainCamera;
    public bool isHolding;

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindObjectOfType<HUD>();
    }

    void Update()
    {
        if (isHolding)
        {
            slots[currentIndex].slotObject.transform.position = holdHand.transform.position;
            slots[currentIndex].slotObject.transform.rotation = mainCamera.transform.rotation;

            if(Input.GetKeyDown(currentKey))
                putAwayItem = true;

            if (Input.GetKeyDown(KeyCode.E))
                dropItem = true;
        }

        //Not ideal but works for now, better solution would be using a switch statement for evaluating and/or a dictionary to store values by key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentKey = KeyCode.Alpha1;
            RetrieveItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentKey = KeyCode.Alpha2;
            RetrieveItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentKey = KeyCode.Alpha3;
            RetrieveItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentKey = KeyCode.Alpha4;
            RetrieveItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentKey = KeyCode.Alpha5;
            RetrieveItem(4);
        }
    }

    private void FixedUpdate()
    {
        if(dropItem)
        {
            RemoveItem(currentIndex);
            isHolding = false;
            dropItem = false;
        }

        if (putAwayItem)
        {
            slots[currentIndex].slotObject.SetActive(false);
            isHolding = false;
            putAwayItem = false;
        }
    }

    public bool InsertItem(GameObject item)
    {
        if (currentIndex >= 5)
            return false;

        foreach(InventorySlot slot in slots)
        {
            if(!slot.occupied)
            {
                item.GetComponent<Interactable>().collectable = false;
                slot.slotObject = item;
                slot.occupied = true;
                slot.GetComponent<InventorySlot>().CreateSnapshot();
                item.SetActive(false);

                StartCoroutine(hud.Notify("Picked up " + slot.slotObject.name));

                return true;
            }
        }

        return false;
    }

    public void RetrieveItem(int index)
    {
        if (!slots[index].occupied)
            return;

        if(slots[currentIndex].occupied)
        {
            slots[currentIndex].slotObject.SetActive(false);
        }

        slots[index].slotObject.SetActive(true);
        isHolding = true;
        currentIndex = index;
    }

    public void RemoveItem(int index)
    {
        slots[index].slotObject.GetComponent<Interactable>().collectable = true;
        slots[index].slotObject = null;
        slots[index].occupied = false;
        slots[index].EraseSnapshot();
    }
}
