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

    /**
     * Using the rigidbody approach to gravity was problematic due to the accelaration.
     **/
    [SerializeField]
    private float gravity = 9.5f;

    [SerializeField]
    private float correctionOffset = .1f;

    public Rigidbody fixedPoint = null;

    private LineRenderer lineRenderer;

    private Rigidbody[] ropeParts;

    private Vector3[] updatedPositions;

    private bool wasAffectedByPhysics = false;

    // Start is called before the first frame update
    private void Start()
    {
        ropeParts = createRope();
        updatedPositions = ropeParts.Select(rp => rp.position).ToArray();
        lineRenderer = generateLineRenderer();
    }

    private LineRenderer generateLineRenderer()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = ropeThickness;
        lineRenderer.endWidth = ropeThickness;
        lineRenderer.positionCount = ropeParts.Length;
        return lineRenderer;
    }

    private Rigidbody[] createRope()
    {
        List<Rigidbody> ropeParts = new List<Rigidbody>(numRopePoints);
        ropeParts.Add(fixedPoint);
        for (int i = 1; i < numRopePoints; i++)
        {
            Vector3 position = fixedPoint.transform.position + Vector3.down * distanceBetweenPoints * i;
            ropeParts.Add(Instantiate(ropePartType, position, Quaternion.identity));
        }
        return ropeParts.ToArray();
    }

    // Update is called once per frame
    private void Update()
    {
        updatePositions();
        lineRenderer.SetPositions(updatedPositions);

        //for (int i = 0; i < numRopePoints; i++)
            //Debug.Log(2 + " " + updatedPositions[2].y);
    }

    private void updatePositions()
    {
        Vector3 gravEffect = Vector3.down * gravity * Time.deltaTime;
        if (wasAffectedByPhysics)
            updatedPositions = ropeParts.Select(rp => rp.position + gravEffect).ToArray();
        else
            updatedPositions = updatedPositions.Select(p => p + gravEffect).ToArray();
        
        updatedPositions[numRopePoints - 1] += gravEffect;

        float targetDiferences = (numRopePoints - 1) * (distanceBetweenPoints + correctionOffset);
        float currentDiferences = float.MaxValue;
        //while (currentDiferences > targetDiferences)
        //{
        maintainDistanceEdgePoints(false);
        maintainDistancePoints2(updatedPositions);
        maintainDistanceEdgePoints(true);   
        maintainDistanceEdgePoints(false);
        maintainDistancePoints2(updatedPositions);
        maintainDistanceEdgePoints(true);
        /*maintainDistanceEdgePoints(false);
        maintainDistancePoints2(updatedPositions);
        maintainDistanceEdgePoints(true);*/
        /*currentDiferences = 0f;
                for (int i = 1; i < numRopePoints; i++)
                {
                    currentDiferences += (updatedPositions[i] - updatedPositions[i - 1]).magnitude;
                }
        Debug.Log(currentDiferences - targetDiferences);*/
        //}
        wasAffectedByPhysics = false;
    }

    private void accountForGravity()
    {
        Vector3 displacement = ropeParts[1].position - ropeParts[0].position;
        float distance = displacement.magnitude;
        if (distance > distanceBetweenPoints)
            ropeParts[1].AddForce(-1 * Physics.gravity);
    }

    private void maintainDistanceEdgePoints(bool isEndOfRope)
    {
        int fixedIndex = isEndOfRope ? numRopePoints - 2 : 0;
        Vector3 displacement = updatedPositions[fixedIndex + 1] - updatedPositions[fixedIndex];
        float distance = displacement.magnitude;
        //Debug.Log(distance);
        Vector3 pointCorrectionDisplacement = displacement.normalized * Math.Abs(distance - distanceBetweenPoints);
        updatedPositions[fixedIndex + 1] -= pointCorrectionDisplacement;

        //Debug.Log("Point " + (fixedIndex + 1) + ": " + pointCorrectionDisplacement);
        /*Vector3 velocity = ropeParts[fixedIndex + 1].velocity;
        ropeParts[fixedIndex + 1].velocity.Set(velocity.x, -gravity, velocity.y);*/
    }

    private void maintainDistancePoints2(Vector3[] positionsAfterGravity)
    {
        for (int i = 2; i < numRopePoints - 1; i++)
        {
            Vector3 displacement = positionsAfterGravity[i] - positionsAfterGravity[i - 1];
            float distance = displacement.magnitude;
            Vector3 pointCorrectionDisplacement = displacement.normalized * Math.Abs(distance - distanceBetweenPoints) * .5f;
            //positionsAfterGravity[i - 1] += pointCorrectionDisplacement;
            positionsAfterGravity[i] -= pointCorrectionDisplacement;
            ropeParts[i - 1].MovePosition(positionsAfterGravity[i - 1] + pointCorrectionDisplacement);
            ropeParts[i].MovePosition(positionsAfterGravity[i] - pointCorrectionDisplacement);
            /*Vector3 velocity = ropeParts[i].velocity;
            ropeParts[i].velocity.Set(velocity.x, -gravity, velocity.y);*/
        }
    }

    private void FixedUpdate()
    {
        for (int i = 1; i < numRopePoints; i++)
        {
            ropeParts[i].MovePosition(updatedPositions[i]);
        }
        wasAffectedByPhysics = true;
    }
}
