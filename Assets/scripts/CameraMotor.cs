using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float smoothSpeed = 2.5f;
    [SerializeField] float smoothRotation = 100f;
    public Vector3 offset = new Vector3(0, 5.0f, -10.0f);
    [SerializeField] Vector3 rotation = new Vector3(35, 0, 0);
    [SerializeField] bool moveOnX = true;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        if (!moveOnX)
        {
            desiredPosition.x = 0;
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, (smoothSpeed * Time.deltaTime));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), (smoothRotation * Time.deltaTime));
    }
}
