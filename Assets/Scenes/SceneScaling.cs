using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScaling : MonoBehaviour
{

    public GameObject plotcontainer;
    
    // Start is called before the first frame update
    void Start()
    {
        //scale(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void ScaleOne_ego()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        plotcontainer.transform.localPosition += new Vector3(0.0f, -1.5f, 0.0f);

    }

    public void Scaletwo_ego()
    {

        plotcontainer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        plotcontainer.transform.localPosition += new Vector3(0.0f, -1.5f, 0.0f);
        
    }

    public void Scalethree_ego()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        plotcontainer.transform.localPosition += new Vector3(0.0f, -1.5f, 0.0f);

    }

    public void Scalefour_ego()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.2f, 0.1f, 0.2f);
        plotcontainer.transform.localPosition += new Vector3(0.0f, -1.5f, 0.0f);
    }
    public void Scalefive_ego()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.3f, 0.1f, 0.3f);
        plotcontainer.transform.localPosition += new Vector3(0.0f, -1.5f, 0.0f);
       
    }
    public void Scalesix_ego()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.4f, 0.1f, 0.4f);
        plotcontainer.transform.localPosition += new Vector3(0.0f, -1.5f, 0.0f);

    }

    public void ScaleOne_exo()
    {
        //scale_change = new Vector3Int(x, y, z)
        plotcontainer.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        plotcontainer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void Scaletwo_exo()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        plotcontainer.transform.localPosition = new Vector3(0.0f, -1.0f, 0.0f);
    }

    public void Scalethree_exo()
    {
        //scale_change = new Vector3Int(x, y, z);
        plotcontainer.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        plotcontainer.transform.localPosition = new Vector3(0.0f, -1.0f, 0.0f);
    }
}
