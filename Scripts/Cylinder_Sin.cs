using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder_Sin : MonoBehaviour
{
    private  GameObject cylinder_sin;
    public Vector3 scaleChange;
    public Color greenColor, redColor;
    public Color currentColor;
    MeshRenderer myRenderer;
    Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        cylinder_sin = this.gameObject;
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = greenColor;
        greenColor = new Color(0, 255, 0);
        currentColor = greenColor;
        redColor = new Color(255, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Mathf.Sin(Time.time) + 2f;
        float y = 0.002f;
        float z = Mathf.Sin(Time.time) + 2f;

        scaleChange = new Vector3(x/10f, y, z/10f);

        currentColor = new Color((24f/255f), 1f, 1f, 0.1f);     

        Debug.Log(Time.time);
        myRenderer.material.color = currentColor;
        cylinder_sin.transform.localScale = scaleChange;
    }
}
