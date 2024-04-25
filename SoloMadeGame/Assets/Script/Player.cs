using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rbody;
    [SerializeField]int speedZ;
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject Light;
    [SerializeField] GameObject Panel;
    [SerializeField] bool revarse = false;
    [SerializeField] bool slow = false;
    float sCountTime = 0;
    [SerializeField] bool gravity = false;
    public bool move = false;
    public AudioClip revarsesound;
    public AudioClip slowsound;
    public AudioClip movesound;
    public AudioClip gravitysound;
    public AudioClip GLockOpensound;
    public AudioClip Darksound;
    public AudioClip Lightsound;
    public AudioClip OutZonesound;
    public AudioClip Jumpsound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraRotation = camera1.transform;
        Vector3 worldAngle = cameraRotation.eulerAngles;

        if (Input.GetKey("w") && move == true)
        {
            rbody.AddForce(Vector3.forward * speedZ, ForceMode.Force);
        }
        if (Input.GetKey("a") && move == true)
        {
            if (revarse == false)
            {
                rbody.AddForce(Vector3.left * speedZ, ForceMode.Force);
            }
            if (revarse == true)
            {
                rbody.AddForce(Vector3.right * speedZ, ForceMode.Force);
            }
        }
        if (Input.GetKey("s") && move == true)
        {
            rbody.AddForce(Vector3.back * speedZ, ForceMode.Force);
        }
        if (Input.GetKey("d") && move == true)
        {
            if (revarse == false)
            {
                rbody.AddForce(Vector3.right * speedZ, ForceMode.Force);
            }
            if (revarse == true)
            {
                rbody.AddForce(Vector3.left * speedZ, ForceMode.Force);
            }
        }
        if (Input.GetKeyDown("space") && gravity == false && move == true)
        {
            audioSource.PlayOneShot(Jumpsound);
            revarse = true;
        }
        if (Input.GetKeyDown("c") && gravity == false && move == true)
        {
            audioSource.PlayOneShot(Jumpsound);
            revarse = false;
        }
        if (revarse == true)
        {
            Physics.gravity = new Vector3(0, 10.0F, 0);
            worldAngle.z = 180f;
        }
        if (revarse == false)
        {
            Physics.gravity = new Vector3(0, -10.0F, 0);
            worldAngle.z = 0f;
        }
        cameraRotation.eulerAngles = worldAngle; // 回転角度を設定
        if (slow == false)
        {
            speedZ = 4;
        }
        if (slow == true)
        {
            Debug.Log(sCountTime);
            speedZ = 2;
            sCountTime += Time.deltaTime;
            if (sCountTime >= 1.5)
            {
                slow = false;
                sCountTime = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("revarse"))
        {//強制的に重力を反転させる(反転ボタン関係なし)
            audioSource.PlayOneShot(revarsesound);
            if (revarse == true)
            {
                revarse = false;
            }
            if (revarse == false)
            {
                revarse = true;
            }
        }
        if (other.gameObject.CompareTag("slow"))
        {
            audioSource.PlayOneShot(slowsound);
            slow = true;//スピードを落とさせる。
        }
        if (other.gameObject.CompareTag("back"))
        {
            audioSource.PlayOneShot(movesound);
            rbody.AddForce(Vector3.back * speedZ * 20, ForceMode.Force);//一マス戻す。
        }
        if (other.gameObject.CompareTag("go"))
        {
            audioSource.PlayOneShot(movesound);
            rbody.AddForce(Vector3.forward * speedZ * 20, ForceMode.Force);//一マス進む。
        }
        if (other.gameObject.CompareTag("gravity"))
        {
            audioSource.PlayOneShot(gravitysound);
            gravity = true;//重力反転不可能になる。
        }
        if (other.gameObject.CompareTag("GLockOpen"))
        {
            audioSource.PlayOneShot(GLockOpensound);
            gravity = false;//重力反転可能になる。
        }
        if (other.gameObject.CompareTag("Dark"))
        {
            audioSource.PlayOneShot(Darksound);
            Light.gameObject.SetActive(false);//闇状態になる
        }
        if (other.gameObject.CompareTag("Light"))
        {
            audioSource.PlayOneShot(Lightsound);
            Light.gameObject.SetActive(true);//元に戻る
        }
        if (other.gameObject.CompareTag("OutZone"))
        {
            audioSource.PlayOneShot(OutZonesound);
            move = false;//ゲームオーバーにする
            Panel.gameObject.SetActive(true);
        }
    }

    public void startButton()
    {
        move = true;
    }
}
