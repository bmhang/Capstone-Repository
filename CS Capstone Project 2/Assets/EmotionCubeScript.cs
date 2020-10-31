using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionCubeScript : MonoBehaviour
{
    public int[] vectorValues = new int[] { 1, 0, 0 };
    private float smooth;
    public float durationTime;

    private float floatTime;

    //This is a sphere
    public GameObject sphere;
    public GameObject cube;

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
        if(floatTime >= 2 * Mathf.PI)
        {
            floatTime = 0;
        }
        this.transform.position = new Vector3(this.transform.position.x, Mathf.Sin(floatTime) / 12 + 1.8f, this.transform.position.z);
    }
}
