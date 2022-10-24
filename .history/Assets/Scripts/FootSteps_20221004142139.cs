using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    CharacterController controller;
    AudioSource audio;

    bool prevGrounded = true;


    [SerializeField, Range(0, 0.1f)]
    private float amplitude = 0.015f;

    [SerializeField, Range(0, 30f)]
    private float freaquency = 10.0f;

    [SerializeField]
    private Transform camera = null;

    [SerializeField]
    private Transform cameraHolder = null;

    private float toggleSpeed = 3.0f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
        startPosition = camera.localPosition;
    }

    private Vector3 FootStepMotion() {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * freaquency) * amplitude;
        pos.x += Mathf.Cos(Time.time * freaquency / 2) * amplitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && !prevGrounded && audio.time > 0.25f)
        {
            audio.volume = 1.5f;
            audio.pitch = 1.2f;
            audio.Play();
        }

        if (controller.isGrounded && controller.velocity.y > 0.1f && audio.time > 0.25f)
        {
            audio.Stop();
            audio.Play();
        }

        if (controller.velocity.x > 10.0f && audio.time > 0.25f)
        {
            audio.Stop();
            audio.Play();
        }

        else if (controller.isGrounded && controller.velocity.sqrMagnitude > 10f && (audio.isPlaying == false || !prevGrounded))
        {
            audio.volume = Random.Range(0.8f, 1.0f);
            audio.pitch = Random.Range(0.8f, 1.0f);
            audio.Play();
        }

        prevGrounded = controller.isGrounded;
    }
}
