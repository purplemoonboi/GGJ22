using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private Interactable interactingObject;
    private InventoryManager inventory;
    private bool collected = false;
    private bool interacted = false;

    [Header("Interaction")]
    [SerializeField]
    private GameObject keyText;
    [SerializeField]
    private GameObject commandText;
    [SerializeField]
    private Camera camera;

    [Header("Menus")]
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private GameObject expandedCrosshair;
    [SerializeField]
    private GameObject notificationText;

   // [SerializeField]
   // private AnimateCollectable animScriptRef;

    void Start()
    {
       // animScriptRef = GetComponent<AnimateCollectable>();
        inventory = FindObjectOfType<InventoryManager>();
    }

    void Update()
    {
         if (interactingObject != null)
            {
                crosshair.SetActive(false);
                expandedCrosshair.SetActive(true);

                if (interactingObject.collectable && collected)
                {
                    if (collected)
                    {
                        interactingObject.gameObject.SetActive(false);
                    }
                }
            }

            else
            {
                crosshair.SetActive(true);
                expandedCrosshair.SetActive(false);
            }

            //Checking for interactions and processing them have to be separate because key presses may not always trigger in FixedUpdate
            ProcessInteraction();
        
    }

    private void FixedUpdate()
    {
        
            CheckInteractables();
        
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
                    interactingObject = hitItem;
                    interacted = true;
                }
            }

            else
            {
                interactingObject = null;
                keyText.SetActive(false);
                commandText.SetActive(false);
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
                if (inventory.InsertItem(item.gameObject))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void ProcessInteraction()
    {
        if(interactingObject != null)
        {
            collected = false;
            string kText = interactingObject.gameObject.GetComponent<Interactable>().getKeyText();
            if (kText != null)
            {
                keyText.GetComponent<Text>().text = kText;
            }

            string cText = interactingObject.gameObject.GetComponent<Interactable>().getCommandText();
            if (cText != null)
            {
                commandText.GetComponent<Text>().text = cText;
            }

            keyText.SetActive(true);
            commandText.SetActive(true);

            collected = CheckCollectable(interactingObject);

            if (interactingObject.collectable && collected)
            {
                interactingObject.gameObject.SetActive(false);
            }

            interacted = false;
        }
    }

    public IEnumerator Notify(string text)
    {
        notificationText.GetComponent<Text>().text = text;
        notificationText.GetComponent<Animator>().SetBool("isNotifying", true);
        yield return new WaitForSeconds(1.75f);
        notificationText.GetComponent<Animator>().SetBool("isNotifying", false);
        StopCoroutine(Notify(text));
    }

    public Interactable GetCurrentInteractingObject()
    {
        return interactingObject;
    }
}
