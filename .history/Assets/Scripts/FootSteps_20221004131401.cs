using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;

    bool prevGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && !prevGrounded)
        {
            audio.Play();
        }
        if (controller.isGrounded && (controller.velocity.sqrMagnitude > 10f || controller.velocity > 0.) && (audio.isPlaying == false || !prevGrounded) )
        {
            audio.volume = Random.Range(0.8f, 1.0f);
            audio.pitch = Random.Range(0.8f, 1.0f);
            audio.Play();
        }

        prevGrounded = controller.isGrounded;
    }
}
