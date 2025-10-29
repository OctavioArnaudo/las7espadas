using UnityEngine;

public class SnakeFocus : GameReload
{

    private float minZoom = 30f;
    private float maxZoom = 60f;
    private float zoomSpeed = 10f;

    private int inPriority = 10;
    private int enPriority = 20;
    private int exPriority = 5;

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.CompareTag(playerPGO.tag))
        {
            vcam.Priority = enPriority;
        }
    }

    protected override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        if (collider.CompareTag(playerPGO.tag))
        {
            vcam.Priority = exPriority;
        }
    }

    protected override void Start()
    {
        base.Start();
        vcam.Priority = inPriority;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Alternar prioridades
            /*
            int temp = cam1.Priority;
            cam1.Priority = cam2.Priority;
            cam2.Priority = temp;
            */
        }

        fov -= scrollInput * zoomSpeed;
        cam.fieldOfView -= scrollInput * zoomSpeed;
        fov = Mathf.Clamp(fov, minZoom, maxZoom);
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        vcam.Lens.FieldOfView = fov;

        transform.position = screenPosition;
    }

}