using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraConfigurator : MonoBehaviour
{
    [SerializeField] GameObject projectionTogglerButtonText, orthographicConfiguratorGameObject, pespectiveConfiguratorGameObject;
    [SerializeField] Slider fovSlider, sizeSlider, ncpSlider, fcpSlider;
    [SerializeField] Camera mainCamera;

    bool orthographicProjection;
    [Range(0, 180)] float fov;
    [Range(0, 100)] float orthographicSize;
    [Range(0, 100)] float nearClipPlane;
    [Range(0, 1000)] float farClipPlane;

    private void Start()
    {
        if (mainCamera != null)
        {
            orthographicProjection = mainCamera.orthographic;
            fovSlider.value = fov = mainCamera.fieldOfView;
            sizeSlider.value = orthographicSize = mainCamera.orthographicSize;
            ncpSlider.value = nearClipPlane = mainCamera.nearClipPlane;
            fcpSlider.value = farClipPlane = mainCamera.farClipPlane;

            Debug.Log(orthographicProjection);
            Debug.Log(orthographicSize);
            Debug.Log(fov);
            Debug.Log(orthographicSize);
            Debug.Log(nearClipPlane);
            Debug.Log(farClipPlane);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(GetComponent<Camera>().aspect, 1.0f, 1.0f));
        Gizmos.DrawFrustum(Vector3.zero, GetComponent<Camera>().fieldOfView, GetComponent<Camera>().farClipPlane, GetComponent<Camera>().nearClipPlane, 1.0f);
    }

    public void toggleCameraProjection()
    {
        orthographicProjection = mainCamera.orthographic = !orthographicProjection;
        updateGUIElements();
    }

    public void updateSize(float _value)
    {
        sizeSlider.value = orthographicSize = mainCamera.orthographicSize = _value;
    }

    public void updateFov(float _value)
    {
        fovSlider.value = fov = mainCamera.fieldOfView = _value;
    }

    public void updateNcp(float _value)
    {
        ncpSlider.value = nearClipPlane = mainCamera.nearClipPlane = _value;
    }
    public void updateFcp(float _value)
    {
        fcpSlider.value = farClipPlane = mainCamera.farClipPlane = _value;
    }

    private void updateGUIElements()
    {
        orthographicConfiguratorGameObject.SetActive(orthographicProjection);
        pespectiveConfiguratorGameObject.SetActive(!orthographicProjection);
        projectionTogglerButtonText.GetComponent<TextMeshProUGUI>().text = orthographicProjection ? "Orthographic" : "Perspective";
    }
}
