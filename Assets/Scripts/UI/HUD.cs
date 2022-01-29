using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private Interactable interactingObject;
    private InventoryManager inventory;
    private bool collected = false;

    [Header("Interaction")]
    public GameObject keyText;
    public GameObject commandText;
    public Camera camera;

    [Header("Menus")]
    public GameObject crosshair;
    public GameObject expandedCrosshair;
    public GameObject notificationText;

    void Start()
    {
        inventory = GameObject.FindObjectOfType<InventoryManager>();
    }

    void Update()
    {
        CheckInteractables();

        if (interactingObject != null)
        {
            crosshair.SetActive(false);
            expandedCrosshair.SetActive(true);

            if(interactingObject.collectable && collected)
            {
                interactingObject.gameObject.SetActive(false);
            }
        }

        else
        {
            crosshair.SetActive(true);
            expandedCrosshair.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        //CheckInteractables();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(camera.transform.position, camera.transform.forward * 100);
    }

    public void CheckInteractables()
    {
        Vector3 origin, direction;
        Ray ray;
        RaycastHit hit;

        origin = camera.transform.position;
        direction = camera.transform.forward;
        ray = new Ray(origin, direction);
        int layerMask = 1 << 2;
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
        {
            Interactable hitItem = hit.collider.GetComponent<Interactable>();

            if (hitItem != null && hitItem.isActiveAndEnabled)
            {
                if (hit.distance <= hitItem.distance)
                {
                    collected = false;
                    keyText.GetComponent<Text>().text = hitItem.gameObject.GetComponent<Interactable>().getKeyText();
                    commandText.GetComponent<Text>().text = hitItem.gameObject.GetComponent<Interactable>().getCommandText();
                    interactingObject = hitItem;
                    keyText.SetActive(true);
                    commandText.SetActive(true);

                    collected = CheckCollectable(hitItem);
                }
            }

            if (hitItem.collectable && collected)
            {
                hitItem.gameObject.SetActive(false);
            }
        }

        else
        {
            interactingObject = null;
            keyText.SetActive(false);
            commandText.SetActive(false);
        }
    }

    public bool CheckCollectable(Interactable item)
    {
        if (!inventory.isHolding && item.collectable)
        {
            if (Input.GetKeyDown(item.key))
            {
                inventory.InsertItem(item.gameObject);

                return true;
            }
        }

        return false;
    }

    public IEnumerator Notify(string text)
    {
        notificationText.GetComponent<Text>().text = text;
        notificationText.GetComponent<Animator>().SetBool("isNotifying", true);
        yield return new WaitForSeconds(3);
        notificationText.GetComponent<Animator>().SetBool("isNotifying", false);
        StopCoroutine(Notify(text));
    }

    public Interactable GetCurrentInteractingObject()
    {
        return interactingObject;
    }
}
