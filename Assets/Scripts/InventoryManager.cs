using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public GameObject holdHand;
    public GameObject camera;
    public bool isHolding;
    private int currentIndex = 0;
    private KeyCode currentKey;
    private bool dropItem = false;
    private bool putAwayItem = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            slots[currentIndex].slotObject.transform.position = holdHand.transform.position;
            slots[currentIndex].slotObject.transform.rotation = camera.transform.rotation;

            if(Input.GetKeyDown(currentKey))
                putAwayItem = true;

            if (Input.GetKeyDown(KeyCode.E))
                dropItem = true;
        }

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

    public void InsertItem(GameObject item)
    {
        if (currentIndex >= 5)
            return;

        foreach(InventorySlot slot in slots)
        {
            if(!slot.occupied)
            {
                item.GetComponent<Interactable>().collectable = false;
                item.SetActive(false);
                slot.slotObject = item;
                slot.occupied = true;
                //slot.CreatePreview();
                break;
            }
        }
    }

    public void RetrieveItem(int index)
    {
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
    }
}
