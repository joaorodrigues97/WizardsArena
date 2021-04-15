using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionControllerPlayer2 : MonoBehaviour
{
    private CharacterController controller = null;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move(new Vector3(1, 0, 0) * 5 * Time.deltaTime);
    }
}
