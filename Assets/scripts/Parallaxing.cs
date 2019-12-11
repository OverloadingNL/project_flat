using System;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;
    public float smoothing = 1f;
    public float correctionFctor = 1f;

    private float[] parallaxScales;
    private Transform cam;
    private ScreenBox screenBox;
    private float relativePosition;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

        screenBox = transform.GetComponent<ScreenBox>();
        relativePosition = cam.InverseTransformPoint(screenBox.transform.position).x;
    }

    void FixedUpdate()
    {
        if (screenBox.isActive)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (relativePosition - cam.InverseTransformPoint(screenBox.transform.position).x) * -1 * parallaxScales[i];

                float backgroundTargetPosX = backgrounds[i].position.x + parallax;

                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

        }
        else
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].position = new Vector3(screenBox.transform.position.x, backgrounds[i].position.y, backgrounds[i].position.z);
            }

        }
        relativePosition = cam.InverseTransformPoint(screenBox.transform.position).x;

    }
}