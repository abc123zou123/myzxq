using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool isOpen = false;

    public void OnCloseOrOpen(GameObject obj)
    {
       var allSrings = gameObject.GetComponentsInChildren<UnityChan.SpringManager>();
        foreach(var spring in allSrings)
        {
            spring.enabled = isOpen;
            
        }
        if (isOpen == false)
        {
            obj.GetComponent<UnityEngine.UI.Text>().text = "点击开启";
        }
        else
        {
            obj.GetComponent<UnityEngine.UI.Text>().text = "点击关闭";
        }
       
        isOpen = !isOpen;
    }
}
