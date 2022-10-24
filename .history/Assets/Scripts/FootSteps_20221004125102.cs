using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;

    bool inAir;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        inAir = !controller.isGrounded;
        if (inAir)
        {
            
        }
        else if (controller.isGrounded && controller.velocity.sqrMagnitude > 10f && audio.isPlaying == false)
        {
            audio.volume = Random.Range(0.8f, 1.0f);
            audio.pitch = Random.Range(0.8f, 1.0f);
            audio.Play();
        }
    }
}
