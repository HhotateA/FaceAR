using UnityEngine;


namespace VRoidSDK.Example
{
    public class ModelRotationController : MonoBehaviour
    {
        Vector3 lastMousePosition;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                transform.rotation *= Quaternion.Euler(0, (lastMousePosition - Input.mousePosition).x / Screen.width * 180, 0);
                lastMousePosition = Input.mousePosition;
            }
        }

        public void Reset()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}