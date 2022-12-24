using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirePrefab : MonoBehaviour
{
    public Material[] colorMaterial;
    public GameObject fireIgniter;

    public TMPro.TMP_Dropdown prefabColor;

    // Start is called before the first frame update
    void Start()
    {
        GetOutput();
    }

    // Update is called once per frame
    public void GetOutput()
    {
        switch (prefabColor.value)
        {
            case 0:
                fireIgniter.GetComponent<MeshRenderer>().material = colorMaterial[0]; // yellow
                break;
            case 1:
                fireIgniter.GetComponent<MeshRenderer>().material = colorMaterial[1]; // orange
                break;
            case 2:
                fireIgniter.GetComponent<MeshRenderer>().material = colorMaterial[2]; // red
                break;
            default:
                break;
        }
    }
}
