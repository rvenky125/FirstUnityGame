using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && controller.velocity.x > 0.1f && audio.isPlaying == false)
        {
            audio.volume = Random.Range(0.8f, 1.0f);
            audio.pitch = Random.Range(0.8f, 1.0f);
            audio.Play();
        }
    }
}
