using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{    CharacterController controller;
    AudioSource _audio;

    bool prevGrounded = true;


    [SerializeField, Range(0, 0.1f)]
    private float amplitude = 0.015f;

    [SerializeField, Range(0, 30f)]
    private float freaquency = 10.0f;

    [SerializeField]
    private Transform _camera = null;

    [SerializeField]
    private Transform _cameraHolder = null;

    private float toggleSpeed = 3.0f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        _audio = GetComponent<AudioSource>();
        startPosition = _camera.localPosition;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * freaquency) * amplitude;
        pos.x += Mathf.Cos(Time.time * freaquency / 2) * amplitude * 2;

        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    void PlayMotion(Vector3 value)
    {
        _camera.localPosition += value;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == startPosition) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPosition, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMotion();
        ResetPosition();
        // _camera.LookAt(FocusTarget());

        if (controller.isGrounded && !prevGrounded && _audio.time > 0.30f)
        {
            _audio.volume = 1.5f;
            _audio.pitch = 1.2f;
            _audio.Play();
        }

        if (controller.isGrounded && controller.velocity.y > 0.1f && _audio.time > 0.30f)
        {
            _audio.Stop();
            _audio.Play();
        }

        if (controller.velocity.x > 10.0f && _audio.time > 0.30f)
        {
            _audio.Stop();
            _audio.Play();
        }

        else if (controller.isGrounded && controller.velocity.sqrMagnitude > 10f && (_audio.isPlaying == false || !prevGrounded))
        {
            _audio.volume = Random.Range(0.8f, 1.0f);
            _audio.pitch = Random.Range(0.8f, 1.0f);
            _audio.Play();
        }

        prevGrounded = controller.isGrounded;
    }
}
