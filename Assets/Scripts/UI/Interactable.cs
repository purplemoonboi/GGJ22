using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactive")]
    public KeyCode key;
    public string commandText;
    public float distance;
    public bool collectable;

    void Start()
    {

    }

    void Update()
    {

    }

    public void setDistance(float distance)
    {
        this.distance = distance;
    }

    public float getDistance()
    {
        return distance;
    }

    public void setKey(KeyCode key)
    {
        this.key = key;
    }

    public void setCommandText(string commandText)
    {
        this.commandText = commandText;
    }

    public KeyCode getKey()
    {
        return key;
    }

    public string getKeyText()
    {
        return key.ToString();
    }

    public string getCommandText()
    {
        return commandText;
    }
}
