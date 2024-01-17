using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endRecieve : MonoBehaviour
{
    private int testInt;
    private bool booleanValueEnd;
    private bool filled;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Conencted");

        var temp = other.GetComponent<WireScript>();
        if (other.gameObject.CompareTag("Wire"))
        {
            setBooleanValueSend(temp.getBooleanValue());
            setFilled(true);
            //Debug.Log("BooleanValueEnd");

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Disconnected");

        var temp = other.GetComponent<WireScript>();
        if (other.gameObject.CompareTag("Wire"))
        {
            setBooleanValueSend(temp.getBooleanValue());
            setFilled(false);
            //Debug.Log("BooleanValueEnd");

        }
    }

    public int getInt()
    {
        return testInt;
    }

    public bool getBooleanValueEnd(){
        return booleanValueEnd;
    }

    public void setBooleanValueSend(bool newVal){
        booleanValueEnd = newVal;
    }

    public bool getFilled(){
        return filled;
    }

    public void setFilled(bool newVal){
        filled = newVal;
    }
    
}
