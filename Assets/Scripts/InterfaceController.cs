using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public Texture2D crosshairImage;

    public float DistanceToFind = 20;

    RaycastHit hitInfo;
    private string sObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * DistanceToFind, Color.red);

        Ray rayTosend = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(rayTosend, out hitInfo, DistanceToFind))
        {
            GameObject HitObj = hitInfo.transform.gameObject;
            sObject = HitObj.name;
            if (HitObj.tag == "Button")
            {
                if (Input.GetMouseButton(0))
                {
                    HitObj.GetComponent<InterfaceModule>().DoAction = true;
                }
            }

        }
        else
        {
            sObject = null;
        }

        
    }
    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);

        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);

        GUI.Label(new Rect(10, 10, 100, 20), sObject);
    }
}
