//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class Shoot : MonoBehaviour
//{
//    public GameObject prefab;  // The GameObject you want to instantiate (e.g., a sphere)
//    public ActionBasedController rightController;  // Reference to the right hand controller

//    void Update()
//    {
//        // Check if the Right Trigger (Select action) is pressed
//        //if (rightController.selectAction.action.ReadValue<float>() > 0.1f)
//        //{
//        //    // Instantiate the prefab at the right controller's position and rotation
//        //    InstantiateAtRightHand();
//        //}
//        if (rightController.activateAction.action.ReadValue<float>() > 0.1f)
//        {
//            // Instantiate the prefab at the right controller's position and rotation
//            InstantiateAtRightHand();
//        }
//    }

//    void InstantiateAtRightHand()
//    {
//        // Get the right controller's position and rotation
//        Vector3 rightHandPosition = rightController.transform.position;
//        Quaternion rightHandRotation = rightController.transform.rotation;

//        // Instantiate the prefab at the right hand's position and rotation
//        Instantiate(prefab, rightHandPosition, rightHandRotation);
//    }
//}

//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class Shoot : MonoBehaviour
//{
//    public GameObject prefab;  // The GameObject to instantiate
//    public ActionBasedController rightController;  // Reference to the right hand controller

//    private bool triggerPressed = false;  // Tracks whether the trigger is pressed

//    void Update()
//    {
//        // Read the current value of the right trigger (activate action)
//        float triggerValue = rightController.activateAction.action.ReadValue<float>();

//        // If the trigger is pressed and wasn't already pressed
//        if (triggerValue > 0.1f && !triggerPressed)
//        {
//            // Set the trigger as pressed and instantiate the object
//            triggerPressed = true;
//            InstantiateAtRightHand();
//        }
//        // If the trigger is released (value is low), reset the state
//        else if (triggerValue <= 0.1f && triggerPressed)
//        {
//            triggerPressed = false;  // Allow for a new instantiation on the next press
//        }
//    }

//    void InstantiateAtRightHand()
//    {
//        // Get the right controller's position and rotation
//        Vector3 rightHandPosition = rightController.transform.position;
//        Quaternion rightHandRotation = rightController.transform.rotation;

//        // Instantiate the prefab at the right hand's position and rotation
//        Instantiate(prefab, rightHandPosition, rightHandRotation);
//    }
//}



using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public GameObject prefab;  // The GameObject to instantiate
    public ActionBasedController rightController;  // Reference to the right hand controller
    public float shootForce = 10f;  // Force to apply to the instantiated object

    private bool triggerPressed = false;  // Tracks whether the trigger is pressed

    void Update()
    {
        // Read the current value of the right trigger (activate action)
        float triggerValue = rightController.activateAction.action.ReadValue<float>();

        // If the trigger is pressed and wasn't already pressed
        if (triggerValue > 0.1f && !triggerPressed)
        {
            // Set the trigger as pressed and instantiate the object
            triggerPressed = true;
            InstantiateAndShoot();
        }
        // If the trigger is released (value is low), reset the state
        else if (triggerValue <= 0.1f && triggerPressed)
        {
            triggerPressed = false;  // Allow for a new instantiation on the next press
        }
    }

    void InstantiateAndShoot()
    {
        // Get the right controller's position and rotation
        Vector3 rightHandPosition = rightController.transform.position;
        Quaternion rightHandRotation = rightController.transform.rotation;

        // Instantiate the prefab at the right hand's position and rotation
        GameObject instantiatedObject = Instantiate(prefab, rightHandPosition, rightHandRotation);

        // Check if the instantiated object has a Rigidbody component
        Rigidbody rb = instantiatedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply velocity in the direction of the right controller's forward vector
            Vector3 shootDirection = rightController.transform.forward;
            rb.velocity = shootDirection * shootForce;
        }
    }
}
