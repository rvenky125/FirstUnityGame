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
            audio.volume = 1.5f;
            audio.pitch = 1.2f;
            audio.Play();
        }

        if (controller.isGrounded && controller.velocity.y > 0.1f)
        {
            audio.Stop();
            audio.Play();
        }

        if (controller.isGrounded && controller.velocity.sqrMagnitude > 10f && (audio.isPlaying == false || !prevGrounded))
        {
            audio.volume = Random.Range(0.8f, 1.0f);
            audio.pitch = Random.Range(0.8f, 1.0f);
            audio.Play();
        }

        prevGrounded = controller.isGrounded;
    }
}
