using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraTouchControl : MonoBehaviour, IDragHandler
{
    public Transform valve;
    public Image focusButton; 

    public float sensitivity;
    public float zoomAmount;
    public float zoomInMax = 0.3f;
    public float zoomOutMax = 1f;


    void Update()
    {
        //zoom(Input.GetAxis("Mouse ScrollWheel"));
    }
    
    public void OnDrag(PointerEventData data)
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.001f);
        }
        else
        {
            float xRotation = sensitivity * (data.position.x - data.pressPosition.x) / Screen.width;
            float yRotation = sensitivity * (data.position.y - data.pressPosition.y) / Screen.height;
            valve.Rotate(Vector3.back, xRotation);
            valve.Rotate(Vector3.right, yRotation);
        }

        if (!focusButton.enabled)
        {
            focusButton.enabled = true;
        }
    }

    void zoom(float increment)
    {
        increment *= zoomAmount;
        Vector3 pos = Camera.main.transform.position;
        if (pos.z+increment < -zoomInMax && pos.z + increment > -zoomOutMax)
        {           
            Camera.main.transform.position = new Vector3(pos.x, pos.y, pos.z + increment);
        }     
    }

    public void ReFocus()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -0.6f);
        valve.eulerAngles = new Vector3(-5, 0, 0);
        focusButton.enabled = false;
    }
}
