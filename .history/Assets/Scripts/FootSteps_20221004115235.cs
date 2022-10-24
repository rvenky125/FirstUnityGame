using UnityEngine;

public class FootSteps : MonoBehaviour
{
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && controller.velocity > 2f && audio.playing == false)
        {

        }
    }
}
