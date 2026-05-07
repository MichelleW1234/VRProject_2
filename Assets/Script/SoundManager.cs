using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource motorsound;
    [SerializeField]
    private AudioClip motorsoundClip;
    [SerializeField]
    private AudioSource countdownSound;
    [SerializeField]
    private AudioClip countdownSoundClip;
    [SerializeField]
    private AudioSource checkpointReachedSound;
    [SerializeField]
    private AudioClip checkpointReachedSoundClip;
    [SerializeField]
    private AudioSource droneCrash;
    [SerializeField]
    private AudioClip droneCrashClip;
    [SerializeField]
    private AudioSource finishSound;
    [SerializeField]
    private AudioClip finishSoundClip;


    [SerializeField]
    private float idlePitch = 0.2f;
    [SerializeField]
    private float movingPitch = 0.8f;
    [SerializeField]
    private float pitchChangeSpeed = 3f;


    private Vector3 lastPosition;


    //DroneManager droneManager;
    //GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPosition = transform.position;

        //droneManager = GetComponent<DroneManager>();
        //gameManager = GetComponent<GameManager>();

        if (motorsound == null)
        {
            Debug.Log("Motor AudioSource is NOT assigned");
            return;
        }

        if (motorsoundClip == null)
        {
            Debug.Log("Motor AudioClip is NOT assigned");
            return;
        }

        motorsound.clip = motorsoundClip;
        motorsound.loop = true;
        motorsound.pitch = idlePitch;
        motorsound.volume = 0.8f;

        Debug.Log("Motor sound started: " + motorsound.isPlaying);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMotorSound();
        lastPosition = transform.position;

    }

    private void UpdateMotorSound()
    {
        if (motorsound == null) return;

        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = distanceMoved > 0.001f && Time.timeScale > 0f;

        if (isMoving)
        {
            if (!motorsound.isPlaying)
            {
                motorsound.pitch = idlePitch;
                motorsound.Play();
            }

            motorsound.pitch = Mathf.Lerp(
                motorsound.pitch,
                movingPitch,
                pitchChangeSpeed * Time.unscaledDeltaTime
            );
        }
        else
        {
            if (motorsound.isPlaying)
            {
                motorsound.pitch = Mathf.Lerp(
                    motorsound.pitch,
                    idlePitch,
                    pitchChangeSpeed * Time.unscaledDeltaTime
                );

            }
        }
    }

    public void PlayCountdownSound()
    {
        if (countdownSound != null && countdownSoundClip != null)
        {
            countdownSound.PlayOneShot(countdownSoundClip);
        }
    }

    public void PlayCheckpointReachedSound()
    {
        if (checkpointReachedSound != null && checkpointReachedSoundClip != null)
        {
            checkpointReachedSound.PlayOneShot(checkpointReachedSoundClip);
        }
    }

    public void PlayDroneCrashSound()
    {
        if (droneCrash != null && droneCrashClip != null)
        {
            droneCrash.PlayOneShot(droneCrashClip);
        }
    }

    public void PlayFinishSound()
    {
        if (finishSound != null && finishSoundClip != null)
        {
            finishSound.PlayOneShot(finishSoundClip);
        }
    }


}
