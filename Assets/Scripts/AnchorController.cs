using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorController : MonoBehaviour {

    public GameObject Anchor;
    public float anchorSpeed = 5;
    public GameObject AnchorSpawnLocation;
    public GameObject AnchorEndLocation;

    private InterfaceModule IM;

    private GameObject AnchorClone;
    private bool AnchorExist;
    private bool MoveAnchor;
	// Use this for initialization
	void Start () {
        IM = gameObject.GetComponent<InterfaceModule>();
        MoveAnchor = false;
        AnchorExist = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (IM.DoAction)
        {
            
            if (!AnchorExist)
            {
                CreateAnchor();
                MoveAnchor = true;
                
            }
            
            IM.DoAction = false;
        }

        if (MoveAnchor)
        {
            AnchorClone.transform.position = Vector3.MoveTowards(AnchorClone.transform.position, AnchorEndLocation.transform.position, anchorSpeed);
            AnchorClone.transform.Rotate(new Vector3(0, 0, 1));
            if (AnchorClone.transform.position.y >= AnchorEndLocation.transform.position.y)
            {
                Destroy(AnchorClone);
                MoveAnchor = false;
                AnchorExist = false;
            }

        }
    }

    void CreateAnchor()
    {
        AnchorClone = Instantiate(Anchor, AnchorSpawnLocation.transform.position, Quaternion.Euler(-90, 0, 0));
        AnchorExist = true;
    }

    


}
