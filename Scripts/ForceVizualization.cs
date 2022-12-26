using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVizualization : MonoBehaviour
{
    private SocketIn input;
    public Vector3 myScaleDesired;

    public Color greenColor, redColor;
    public Color currentColor;

    public GameObject Penetration;
    public bool spawned = false;

    public float current_position = 0.1f;
    public float previous_position = 10.0f;

    public float current_force = 0.0f;
    public float previous_force = 0.0f;

    public float dataDesiredToSend; 
    MeshRenderer myRenderer;

    Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        input = GameObject.Find("panda").GetComponent<SocketIn>();
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = greenColor;
        currentColor = greenColor;
        greenColor = new Color(0, 255, 0);
        redColor = new Color(255, 0, 0);
        dataDesiredToSend = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        current_force = input.pos;
        current_position = this.transform.position.y;

        Vector3 pen = this.transform.position;

        myScaleDesired = new Vector3(input.pos*0.3f/8.0f, 0.002f, input.pos*0.3f/8.0f);
        currentColor = new Color(input.pos*1f/8.0f, 1f - input.pos * 1f / 8.0f, 0,0.4f);

        if (current_force < (previous_force - 0.2f)  && current_position < previous_position && spawned == false)
        {
            GameObject instantiatedObject = Instantiate(Penetration, pen, Quaternion.Euler(0f, -45.264f, 180f));
            spawned = true;
        }

        dataDesiredToSend = myScaleDesired.x;

        myRenderer.material.color = currentColor;
        this.transform.localScale = myScaleDesired;
        previous_position = current_position;
        previous_force = current_force;

    }
}
