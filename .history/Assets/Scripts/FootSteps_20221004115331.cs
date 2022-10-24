using UnityEngine;

public class FootSteps : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && controller.velocity.y > 2f && audio.isPlaying == false)
        {
            audio.volume = Random.RandomRange()
        }
    }
}
