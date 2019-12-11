using System;
using UnityEngine;

public class ScreenBox : MonoBehaviour
{
    public Vector2 size;
    public Vector2 offset;
    public bool isActive;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    private BoxCollider2D boundingBox;
    private Camera mainCamera;
    private float halfHeight;
    private float halfWidth;

    private PlayerPlatformerController player;

    private void Start()
    {
        CreateBoxcollider();
        SetBoundries();
        SetCameraSettings();

        player = FindObjectOfType<PlayerPlatformerController>();
    }

    private void Update()
    {
        if (IsPlayerInBox())
        {
            isActive = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            isActive = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        if (IsPlayerInBox())
        {
            ClampCameraPosition();
        }
    }

    private bool IsPlayerInBox()
    {
        if (player.transform.position.x > maxBounds.x || player.transform.position.x < minBounds.x)
        {
            return false;
        }
        return true;
    }

    private void ClampCameraPosition()
    {
        float clampedX = Mathf.Clamp(mainCamera.transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(mainCamera.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        mainCamera.transform.position = new Vector3(clampedX, clampedY, mainCamera.transform.position.z);
    }

    private void SetCameraSettings()
    {
        mainCamera = Camera.main;
        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    private void SetBoundries()
    {
        minBounds = boundingBox.bounds.min;
        maxBounds = boundingBox.bounds.max;
    }

    private void CreateBoxcollider()
    {
        boundingBox = transform.gameObject.AddComponent<BoxCollider2D>();
        boundingBox.isTrigger = true;
        boundingBox.offset = offset;
        boundingBox.size = size;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + offset.x, transform.position.y + offset.y), new Vector3(size.x, size.y));
    }
}
