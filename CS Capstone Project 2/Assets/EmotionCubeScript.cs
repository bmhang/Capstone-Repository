using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionCubeScript : MonoBehaviour
{
    public int[] vectorValues = new int[] { 1, 0, 0 };
    private float smooth;
    public float durationTime;

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
        smooth = Time.deltaTime * durationTime;
        cube.gameObject.transform.Rotate(new Vector3(vectorValues[0], vectorValues[1], vectorValues[2]) * smooth);

        
    }
}
