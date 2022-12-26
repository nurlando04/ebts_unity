using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Sinusoidal : MonoBehaviour
{
    private GameObject torus;
    private SocketIn sin_input;
    public Vector3 scaleChange;

    public Color greenColor, redColor;
    public Color currentColor;
    MeshRenderer myRenderer;

    private SocketIn rosTopic;
    public float actualData;

    // Start is called before the first frame update
    void Start()
    {
        sin_input = GameObject.Find("panda").GetComponent<SocketIn>();
        torus = this.gameObject;

        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = greenColor;
        greenColor = new Color(0, 255, 0);
        currentColor = greenColor;
        redColor = new Color(255, 0, 0);

        //rosTopic = this.gameObject.GetComponent<TopicToRos>();
        rosTopic = GameObject.Find("panda").GetComponent<SocketIn>();
        actualData = GameObject.Find("Cylinder").GetComponent<ForceVizualization>().dataDesiredToSend;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualData = GameObject.Find("Cylinder").GetComponent<ForceVizualization>().dataDesiredToSend;

        scaleChange = new Vector3(sin_input.sinus * 0.5f / 8.0f, 0.5f, sin_input.sinus * 0.5f / 8.0f);

        currentColor = new Color((48f / 255f), (79f / 255f), (254f/255f), 0.9f);

        if (sin_input.sinus > 0f)
        {
            currentColor = new Color((236f / 255f), (239f / 255f), (241f / 255f), 0.5f);
        }

        rosTopic.SendMessage(actualData.ToString() + "|" + scaleChange.x.ToString() + '\n');

        myRenderer.material.color = currentColor;
        torus.transform.localScale = scaleChange;
    }
}
