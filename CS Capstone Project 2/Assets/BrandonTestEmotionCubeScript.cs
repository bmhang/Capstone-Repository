using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandonTestEmotionCubeScript : MonoBehaviour
{
    public int[] vectorValues = new int[] { 1, 0, 0 };
    public float durationTime;
    public GameObject sphere;
    public GameObject cube;
    public PMDataTestScript pmDataCatcher;

    private float smooth;
    private float floatTime;

    private float pVal = 0;
    private float aVal = 0;
    private float dVal = 0;

    private Vector3 engV;
    private Vector3 excV;
    private Vector3 focV;
    private Vector3 intV;
    private Vector3 relV;
    private Vector3 strV;

    private Vector3 sphereLocation;
    private bool isSphereMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotating the cube by durationTime
        smooth = Time.deltaTime * durationTime;
        this.gameObject.transform.Rotate(new Vector3(vectorValues[0], vectorValues[1], vectorValues[2]) * smooth);

        //Making the cube float up and down
        floatTime += Time.deltaTime * 1;
        if (floatTime >= 2 * Mathf.PI)
        {
            floatTime = 0;
        }
        this.transform.position = new Vector3(this.transform.position.x, Mathf.Sin(floatTime) / 12 + 1.8f, this.transform.position.z);

        //Excitement = (+P, +A, -D)
        //Engagement = (+P, +A, +D)
        //Meditation = Relaxation = (+P, -A, +D)
        //Frustration = Stress = (-P, +A, -D)

        excV = new Vector3(pmDataCatcher.excitment, pmDataCatcher.excitment, -pmDataCatcher.excitment);
        engV = new Vector3(pmDataCatcher.engagement, pmDataCatcher.engagement, pmDataCatcher.engagement);
        relV = new Vector3(pmDataCatcher.relaxation, -pmDataCatcher.relaxation, pmDataCatcher.relaxation);
        strV = new Vector3(-pmDataCatcher.stress, pmDataCatcher.stress, -pmDataCatcher.stress);

        sphereLocation = excV + engV + relV + strV;
        sphereLocation.Normalize();
        sphereLocation = sphereLocation * 0.5f;

        if (!isSphereMoving)
        {
            StartCoroutine(moveObject());
        }
    }

    private IEnumerator moveObject()
    {
        isSphereMoving = true;

        Vector3 origin = sphere.transform.localPosition;
        float totalMovementTime = 0.5f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        while (Vector3.Distance(sphere.transform.localPosition, sphereLocation) > 0)
        {   
            currentMovementTime += Time.deltaTime;
            sphere.transform.localPosition = Vector3.Lerp(origin, sphereLocation, currentMovementTime / totalMovementTime);
            yield return null;
        }

        isSphereMoving = false;
    }
}
