using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 dir = Vector3.zero;
    public Material BasicColor;

    /*모드컬러*/
    public Material NinjaModeColor;
    public Material ExploModeColor;
    public Material ShieldModeColor;
    public Material BulletTimeModeColor;
    //public Material FakeModeColor;

    //모드 활성화 ON
    public int weaponCount = 1;

    /*파티클 가져오기*/
    public ParticleSystem death_particle;
    public ParticleSystem shield_particle;
    public ParticleSystem dodge_particle;

    //public ParticleSystem Fake_particle;
    public GameObject Explosion_Particle;

    public bool modeActive = false;
    private GameObject SField; //쉴드필드 생성

    public GameObject fakePlayerPrefab;
    //
    //
    //
    //
    //

    private void Update()
    {
        SField = GameObject.Find("ShieldField").gameObject;
        SField.transform.position = gameObject.transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = new Vector3(h, v, 0f);

        transform.Translate(dir * speed * Time.deltaTime);

        //은신+변신 모드추가
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponCount > 0)
        {
            if (modeActive == false)
            {
                modeActive = true;
                StartCoroutine(NinjaMode());
                StartCoroutine(FakeMode());
                weaponCount = --weaponCount;
                Debug.Log("은신+변신");
            }
        }

        /*훼이크 모드추가
        if (Input.GetKeyDown(KeyCode.Alpha4) && weaponCount > 0)
        {
            if (modeActive == false)
            {
                modeActive = true;
                StartCoroutine(FakeMode());
                weaponCount = --weaponCount;
                Debug.Log("훼이크");
            }
        } */

        //폭발 모드추가
        if (Input.GetKeyDown(KeyCode.Alpha2) && weaponCount > 0)
        {
            if (modeActive == false)
            {
                modeActive = true;
                StartCoroutine(ExplosionMode());
                weaponCount = --weaponCount;
                Debug.Log("폭발");
            }
        }

        //쉴드 모드추가
        if (Input.GetKeyDown(KeyCode.Alpha3) && weaponCount > 0)
        {
            if (modeActive == false)
            {
                modeActive = true;
                StartCoroutine(ShieldMode());
                weaponCount = --weaponCount;
                Debug.Log("쉴드");
            }
        }

        //블릿타임 모드추가
        if (Input.GetKeyDown(KeyCode.Alpha4) && weaponCount > 0)
        {
            if (modeActive == false)
            {
                modeActive = true;
                StartCoroutine(BulletTimeMode());
                weaponCount = --weaponCount;
                Debug.Log("블릿타임");
            }
        }
    }

    //닌자모드 실행시 플레이어컬러 교체
    private IEnumerator NinjaMode()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().material = NinjaModeColor;
        dodge_particle.Play();

        Debug.Log("은신모드 전");
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<MeshRenderer>().material = BasicColor;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        modeActive = false;
        dodge_particle.Stop();
        Debug.Log("은신모드 후");
    }

    //훼이크모드 실행시 플레이어컬러 교체
    private IEnumerator FakeMode()
    {
        GameObject BombField = GameObject.Find("FakePlayer");

        //gameObject.GetComponent<MeshRenderer>().material = FakeModeColor;
        GameObject fakeObject = GameObject.Instantiate<GameObject>(fakePlayerPrefab, gameObject.transform.position, Quaternion.identity);
        FakeToEnemy();

        Debug.Log("훼이크전");
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<MeshRenderer>().material = BasicColor;
        modeActive = false;
        Destroy(fakeObject);
        // ReturnToPlayer();

        Debug.Log("훼이크후");
    }

    //폭발모드 실행시 플레이어컬러 교체
    private IEnumerator ExplosionMode()
    {
        GameObject BombField = GameObject.Find("ExplosionField");

        BombField.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().material = ExploModeColor;
        Explosion_Particle.SetActive(true);

        Debug.Log("폭발전");
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<MeshRenderer>().material = BasicColor;
        BombField.GetComponent<CircleCollider2D>().enabled = false;
        modeActive = false;
        Explosion_Particle.SetActive(false);
        Debug.Log("폭발후");
    }

    //쉴드모드 실행시 플레이어컬러 교체
    private IEnumerator ShieldMode()
    {
        GameObject BombField = GameObject.Find("ShieldField");

        BombField.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().material = ShieldModeColor;
        shield_particle.Play();

        Debug.Log("쉴드전");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<MeshRenderer>().material = BasicColor;
        BombField.GetComponent<CircleCollider2D>().enabled = false;
        modeActive = false;
        shield_particle.Stop();
        Debug.Log("쉴드후");
    }

    //블릿타임모드 실행시 플레이어컬러 교체
    private IEnumerator BulletTimeMode()
    {
        gameObject.GetComponent<MeshRenderer>().material = BulletTimeModeColor;
        SlowtoEnemy(0.5f);

        Debug.Log("블릿타임 전");

        yield return new WaitForSeconds(3);
        gameObject.GetComponent<MeshRenderer>().material = BasicColor;
        modeActive = false;
        ReturnToNormalSpeed();

        // ReturnToPlayer();

        Debug.Log("블릿타임 후");
    }

    /*
     *
     *
     *
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Death");
        RestartButton.Show();

        //GetComponentInChildren<ParticleSystem>().Play();
        death_particle.Play();

        StopToEnemy();
        StopToPlayer();

        //Time.timeScale = 0f;

        GetComponent<AudioSource>().Play();
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
    }

    public void StopToEnemy()
    {
        EnemyController[] EnemyCont = GameObject.FindObjectsOfType<EnemyController>(); //EnemyController클래스호출
        EnemyManager EnemyMan = GameObject.FindObjectOfType<EnemyManager>(); //EnemyManager클래스호출
        EnemyMan.gameObject.SetActive(false);
        foreach (EnemyController d in EnemyCont)
        {
            d.enabled = false;
            Debug.Log("정지");
        }
    }

    /* 플레이어가 죽었을 때 Player 정지 */

    public void StopToPlayer()
    {
        PlayerController PlayerCont = GameObject.FindObjectOfType<PlayerController>();
        PlayerCont.enabled = false;
    }

    /* 플레이어가 죽었을 때 Enemy 정지 */

    public void FakeToEnemy()
    {
        EnemyController[] EnemyCont = GameObject.FindObjectsOfType<EnemyController>();
        foreach (EnemyController s in EnemyCont)
        {
            s.ChangeTarget("FakePlayer");
        }
    }

    //적의 속도를 늦추는 코드
    public void SlowtoEnemy(float EnemySpeedDown)
    {
        EnemyController[] EnemyCont = GameObject.FindObjectsOfType<EnemyController>();
        foreach (EnemyController t in EnemyCont)
        {
            t.ChangeE_Speed(EnemySpeedDown);
        }
    }

    public void ReturnToNormalSpeed()
    {
        EnemyController[] EnemyCont = GameObject.FindObjectsOfType<EnemyController>();
        foreach (EnemyController r in EnemyCont)
        {
            r.ReturnE_Speed();
        }
    }

    /*public void ReturnToPlayer() //훼이크모드 종료 후 플레이어로 재타겟
    {
        EnemyController[] EnemyCont = GameObject.FindObjectsOfType<EnemyController>();
        foreach (EnemyController s in EnemyCont)
        {
            s.ChangeTarget("Player");
        }
    } */
}