using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    public GameObject entryCameraTopView; 
    public GameObject entryCameraBottomLeft; 
    public GameObject mainCamera;

    public event Action OnCameraSettingDone;
    private bool skipPressed = false;

    private float transitionDuration = 2f;

    public TransitionManager transitionManager;

    public void ActivateEntryCameraTopView()
    {
        if (entryCameraTopView != null)
        {
            entryCameraTopView.SetActive(true);
        }
    }

    public void StartEntryCameraTopViewZoomIn()
    {
        float delay = 0.5f;
        StartCoroutine(DelayedEntryCameraTopViewZoomIn(delay));
    }

    private IEnumerator DelayedEntryCameraTopViewZoomIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(EntryCameraTopViewZoomIn());
    }

    private IEnumerator EntryCameraTopViewZoomIn()
    {
        if (entryCameraTopView != null)
        {
            Camera camera = entryCameraTopView.GetComponent<Camera>();
            if (camera != null)
            {
                float startFOV = camera.fieldOfView;
                float endFOV = 58f;
                float startTime = Time.time;
                float duration = 1.5f;

                while (Time.time < startTime + duration)
                {
                    if (skipPressed)
                    {
                        yield break;
                    }

                    float t = (Time.time - startTime) / duration;
                    if (camera != null)
                        camera.fieldOfView = Mathf.Lerp(startFOV, endFOV, t);

                    if (duration >= 0.5f && transitionManager != null)
                    {
                        transitionManager.StartFadeInBlack();
                    }

                    yield return null;
                }

                if (camera != null)
                    camera.fieldOfView = endFOV;
            }

            if (OnCameraSettingDone != null)
                OnCameraSettingDone.Invoke();
        }
    }

    public void SwitchToEntryCameraBottomLeft()
    {
        DeactivateEntryCameraTopView();
        ActivateEntryCameraBottomLeft();
    }

    public void DeactivateEntryCameraTopView()
    {
        if (entryCameraTopView != null)
        {
            entryCameraTopView.SetActive(false);
        }
    }

    public void ActivateEntryCameraBottomLeft()
    {
        if (entryCameraBottomLeft != null)
        {
            entryCameraBottomLeft.SetActive(true);
        }
    }

    public void DestroyEntryCameraTopView()
    {
        if (entryCameraTopView != null)
        {
            Destroy(entryCameraTopView);
        }
    }

    public void StartEntryCameraBottomLeftZoomIn()
    {
        StartCoroutine(EntryCameraBottomLeftZoomIn());
    }

    private IEnumerator EntryCameraBottomLeftZoomIn()
    {
        if (entryCameraBottomLeft != null)
        {
            Camera camera = entryCameraBottomLeft.GetComponent<Camera>();
            if (camera != null)
            {
                float startFOV = camera.fieldOfView;
                float endFOV = 23.5f;
                float startTime = Time.time;
                float duration = 1.5f;

                if (transitionManager != null)
                    transitionManager.StartFadeOutBlack();

                while (Time.time < startTime + duration)
                {
                    if (skipPressed)
                    {
                        yield break;
                    }

                    float t = (Time.time - startTime) / duration;
                    if (camera != null)
                        camera.fieldOfView = Mathf.Lerp(startFOV, endFOV, t);

                    yield return null;
                }

                if (camera != null)
                    camera.fieldOfView = endFOV;
            }

            if (OnCameraSettingDone != null)
                OnCameraSettingDone.Invoke();
        }
    }

    public void StartRevolveEntryCamera()
    {
        StartCoroutine(RevolveEntryCamera());
    }

    private IEnumerator RevolveEntryCamera()
    {
        if (entryCameraBottomLeft != null)
        {
            Camera camera = entryCameraBottomLeft.GetComponent<Camera>();
            if (camera != null)
            {
                Vector3 originalPosition = entryCameraBottomLeft.transform.position;
                float startTime;
                float duration;

                Vector3 targetPosition = new Vector3(150f, originalPosition.y, originalPosition.z);
                startTime = Time.time;
                duration = 6.921f;

                while (Time.time < startTime + duration)
                {
                    if (skipPressed)
                    {
                        yield break;
                    }

                    float t = (Time.time - startTime) / duration;
                    if (entryCameraBottomLeft != null)
                        entryCameraBottomLeft.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                    yield return null;
                }

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.transform.position = targetPosition;

                yield return new WaitForSeconds(0.3f);

                originalPosition = entryCameraBottomLeft.transform.position;
                targetPosition = new Vector3(150f, 42.5f, originalPosition.z);
                startTime = Time.time;
                duration = 3.75f;

                while (Time.time < startTime + duration)
                {
                    if (skipPressed)
                    {
                        yield break;
                    }

                    float t = (Time.time - startTime) / duration;
                    if (entryCameraBottomLeft != null)
                        entryCameraBottomLeft.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                    yield return null;
                }

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.transform.position = targetPosition;

                yield return new WaitForSeconds(0.3f);

                originalPosition = entryCameraBottomLeft.transform.position;
                targetPosition = new Vector3(0f, 42.5f, originalPosition.z);
                startTime = Time.time;
                duration = 6.921f;

                while (Time.time < startTime + duration)
                {
                    if (skipPressed)
                    {
                        yield break;
                    }

                    float t = (Time.time - startTime) / duration;
                    if (entryCameraBottomLeft != null)
                        entryCameraBottomLeft.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                    yield return null;
                }

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.transform.position = targetPosition;

                yield return new WaitForSeconds(0.3f);

                originalPosition = entryCameraBottomLeft.transform.position;
                targetPosition = new Vector3(0f, -52.5f, originalPosition.z);
                startTime = Time.time;
                duration = 3.75f;

                while (Time.time < startTime + duration)
                {
                    if (skipPressed)
                    {
                        yield break;
                    }

                    float t = (Time.time - startTime) / duration;
                    if (entryCameraBottomLeft != null)
                        entryCameraBottomLeft.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
                    yield return null;
                }

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.transform.position = targetPosition;

                yield return new WaitForSeconds(.85f);

                if (OnCameraSettingDone != null)
                    OnCameraSettingDone.Invoke();
            }
        }
    }

    public void StartSmoothSwitchToMainCamera()
    {
        StartCoroutine(SmoothSwitchToMainCamera());
    }

    private IEnumerator SmoothSwitchToMainCamera()
    {
        if (entryCameraBottomLeft != null && mainCamera != null)
        {
            float startTime = Time.time;
            Vector3 startPosition = entryCameraBottomLeft.transform.position;
            Vector3 startRotation = entryCameraBottomLeft.transform.rotation.eulerAngles;
            float startFOV = entryCameraBottomLeft.GetComponent<Camera>().fieldOfView;

            Vector3 targetPosition = mainCamera.transform.position;
            Vector3 targetRotation = mainCamera.transform.rotation.eulerAngles;
            float targetFOV = mainCamera.GetComponent<Camera>().fieldOfView;

            while (Time.time < startTime + transitionDuration)
            {
                if (skipPressed)
                {
                    yield break;
                }

                float t = (Time.time - startTime) / transitionDuration;

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, targetRotation, t));

                if (entryCameraBottomLeft != null)
                    entryCameraBottomLeft.GetComponent<Camera>().fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

                yield return null;
            }

            if (entryCameraBottomLeft != null)
            {
                entryCameraBottomLeft.transform.position = targetPosition;
                entryCameraBottomLeft.transform.rotation = Quaternion.Euler(targetRotation);
                entryCameraBottomLeft.GetComponent<Camera>().fieldOfView = targetFOV;
            }

            if (OnCameraSettingDone != null)
                OnCameraSettingDone.Invoke();
        }
    }

    public void SwitchToMainCamera()
    {
        DeactivateEntryCameraBottomLeft();
        ActivateMainCamera();
    }

    public void DeactivateEntryCameraBottomLeft()
    {
        if (entryCameraBottomLeft != null)
        {
            entryCameraBottomLeft.SetActive(false);
        }
    }
    public void ActivateMainCamera()
    {
        if (mainCamera != null)
        {
            mainCamera.SetActive(true);
        }
    }

    public void DestroyEntryCameraBottomLeft()
    {
        if (entryCameraBottomLeft != null)
        {
            Destroy(entryCameraBottomLeft);
        }   
    }

    public void DestroyEntryCameras()
    {
        DestroyEntryCameraTopView();
        DestroyEntryCameraBottomLeft();
    }

    public void OnSkipButtonPress()
    {
        skipPressed = true;
    }
}
