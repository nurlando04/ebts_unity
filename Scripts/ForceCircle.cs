using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCircle : MonoBehaviour
{

    // Start is called before the first frame update
    public float changer = 0;
    public float changerN = 0;

    //private float PrevChanger = 0;
    private ParticleSystem donut;
    //public ParticleSystem.MinMaxGradient minC;
    //public ParticleSystem.MinMaxGradient maxC;
    //private ParticleSystem.ColorOverLifetimeModule colorModule;

    void Start()
    {
        donut = this.gameObject.GetComponent<ParticleSystem>();
        //maxC = this.gameObject.GetComponent<ParticleSystem.MinMaxGradient>().colorMax;
        //minC = this.gameObject.GetComponent<ParticleSystem.MinMaxGradient>().colorMin;
        //colorModule = donut.colorOverLifetime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changer = this.gameObject.GetComponent<SensorSock>().pos;
        ParticleSystem.MainModule settings = this.gameObject.GetComponent<ParticleSystem>().main;
       
        if (changer <= 8.0f)
        {
            //changerN = (changer / 8.0f);
            changerN = (changer / 8.0f);
            //colorModule.color = new ParticleSystem.MinMaxGradient(minC.color, maxC.color);
        }   
        // else if (changer < 0.6f)
        // {
        //     changerN = 0.0f;
        // }
        else
        {
            changerN = 1.0f;
            //settings.startColor = new Color(255f, 0f, 0f);
        }
        donut.Simulate(changerN, true, true);
        donut.Pause(true);
        donut.time = changerN;
    }
}
