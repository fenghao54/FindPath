using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class playerMove : MonoBehaviour {

    public int floorLayer;
    public Vector3 playerMousePos;
    public Vector3[] ways;
    private int maxIndex=50;
    private int curIndex;
    private int Index=0;
   
    public float speed = 10f;
    public Vector3 offset;
    public bool isMove=true;
    void Start ()
	{
	    floorLayer = LayerMask.GetMask("Floor");
        ways=new Vector3[maxIndex];
	}

	void Update ()
    {
        printLine(ways);
        if (Input.GetMouseButtonDown(0))
        {
            ways[curIndex] = GetPos();
            curIndex++;
        }

        if (curIndex > 0)
        {
            if (Index > curIndex - 1)
            {
                return;
            }
            offset = ways[Index] - transform.position;
          
            if (offset.magnitude > 0.1f)
            {
                transform.position += offset.normalized * speed * Time.deltaTime;
            }
            else
            {
                Index++;
            }
        }
    }

    Vector3 GetPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, floorLayer))
        {
            playerMousePos = hit.point;
        }
        return playerMousePos;
    }

    void printLine(Vector3[] path)
    {
        for (int i = 0; i < curIndex; i++)
        {
            if (i == curIndex - 1)
            {
                return;
            }
            Debug.DrawLine(path[i],path[i+1],Color.green);
        }
    }
}
