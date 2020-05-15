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
    private Rigidbody ropePartType;

    [SerializeField]
    public Rigidbody fixedPoint = null;

    /**
     * Using the rigidbody approach to gravity was problematic due to the accelaration.
     **/
    [SerializeField]
    private float gravity = 9.5f;

    private LineRenderer lineRenderer;

    private List<Rigidbody> ropeParts;


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

    private List<Rigidbody> createRope()
    {
        List<Rigidbody> ropeParts = new List<Rigidbody>(numRopePoints);
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
        for (int i = 1; i < numRopePoints; i++)
        {
            /*if (i == 1)
            {
                Debug.Log(i - 1 + " in pos: " + ropeParts[i - 1].position);
                Debug.Log(i + " in pos: " + ropeParts[i].position);
            }*/
            float distance = (ropeParts[i].position - ropeParts[i - 1].position).magnitude;
            Vector3 displacement = (ropeParts[i].position - ropeParts[i - 1].position).normalized * Math.Abs(distance - distanceBetweenPoints);
            /*if (i == 1)
            {
                Debug.Log("Displacement = " + displacement);
                Debug.Log("Distance after the displacement: " + (ropeParts[i].position - ropeParts[i - 1].position - displacement).magnitude);
            }*/
            ropeParts[i].MovePosition(ropeParts[i].position - displacement);
            Vector3 velocity = ropeParts[i].velocity;
            ropeParts[i].velocity.Set(velocity.x, -gravity, velocity.y);
        }
    }

    /*private void maintainDistanceFirstPoint()
    {
    }

    private void maintainDistancePoints()
    {

    }*/
}
