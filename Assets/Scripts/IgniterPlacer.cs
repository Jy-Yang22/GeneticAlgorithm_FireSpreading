using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniterPlacer : MonoBehaviour
{
    private GridManager grid;

    public GameObject igniter;
    public float hitPoint = 5f;
    public Vector3 pos;
    public Vector3 finalPosition;

    public GameObject spawnPoint;

    public bool startFire = false;

    private void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceNear(hitInfo.point);
            }
        }
    }

    public void PlaceNear(Vector3 clickPoint)
    {
        finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
        //Instantiate(igniter, finalPosition, Quaternion.identity);

        var spawnPosition = new Vector3(finalPosition.x, finalPosition.y + 3, finalPosition.z);
        //Instantiate(igniter, spawnPosition, Quaternion.identity);

        spawnPoint.transform.position = finalPosition;
        StartCoroutine(WaitFewSeconds(0.5f));

        //Debug.Log(spawnPosition);
        //Debug.Log(finalPosition);
    }

    IEnumerator WaitFewSeconds(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        spawnPoint.SetActive(true);
        startFire = true;
    }
}
