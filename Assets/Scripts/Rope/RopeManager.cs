using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent(LineRenderer)]
public class RopeManager : MonoBehaviour
{

    [SerializeField]
    private int numRopePoints = 50;

    [SerializeField]
    private float distanceBetweenPoints = .2f;

    [SerializeField]
    private float ropeThickness = .5f;

    [SerializeField]
    private GameObject ropePartType;

    [SerializeField]
    public GameObject fixedPoint = null;

    private LineRenderer lineRenderer;

    private List<GameObject> ropeParts;



    // Start is called before the first frame update
    private void Start()
    {
        ropeParts = createRope();
        lineRenderer = generateLineRenderer();
    }

    private LineRenderer generateLineRenderer()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = ropeThickness;
        lineRenderer.endWidth = ropeThickness;
        lineRenderer.positionCount = ropeParts.Count;
        return lineRenderer;
    }

    private List<GameObject> createRope()
    {
        List<GameObject> ropeParts = new List<GameObject>(numRopePoints);
        ropeParts.Add(fixedPoint);
        for (int i = 1; i < numRopePoints; i++)
        {
            Vector3 position = fixedPoint.transform.position + Vector3.down * distanceBetweenPoints * i;
            ropeParts.Add(Instantiate(ropePartType, position, Quaternion.identity));
        }
        return ropeParts;
    }

    // Update is called once per frame
    private void Update()
    {
        generateFrameRope();
    }

    private void generateFrameRope()
    {
        lineRenderer.SetPositions(ropeParts.Select(go => go.transform.position).ToArray());
    }

    private void FixedUpdate()
    {
        /*for (int i = 1; i < numRopePoints; i++)
        {
            float distance = (ropeParts[i - 1].transform.position - ropeParts[i].transform.position).magnitude;
            Vector3 displacement = Vector3.MoveTowards(ropeParts[i].transform.position, ropeParts[i - 1].transform.position, Math.Abs(distance - distanceBetweenPoints));
            int direction = distance > distanceBetweenPoints ? 1 : -1;
            ropeParts[2].transform.Translate(displacement * direction);
        }*/
    }

    /*private void maintainDistanceFirstPoint()
    {
    }

    private void maintainDistancePoints()
    {

    }*/
}
