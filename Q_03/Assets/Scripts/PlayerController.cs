using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 100)]
    public int Hp { get; private set; }
    private bool _isDeadAudioPlayed; // 죽음 사운드를 재생시켰는지 저장할 bool변수 추가
    private AudioSource _audio;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void TakeHit(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Die();
        }
    }

    Coroutine deactivePlayerRoutine;
    public void Die()
    {
        if(!_isDeadAudioPlayed)
        {
            _audio.Play();
            _isDeadAudioPlayed = true;
            if (deactivePlayerRoutine == null)
                StartCoroutine(DeactivePlayerRoutine());

        }
        

        //gameObject.SetActive(false);
    }

    IEnumerator DeactivePlayerRoutine()
    {
        //while(_audio.isPlaying) //isPlaying을 썼더니 비명소리가 끝나기 전에 한번 더 죽어서 계속 비명소리가 이어지며 플레이어가 죽지 않음
        //    yield return null;
        yield return new WaitForSeconds(_audio.clip.length); // 이렇게 하니 한번 비명소리가 끝나고 바로 죽음 

        gameObject.SetActive(false);
            
    }

    private void OnDisable()
    {
        if(deactivePlayerRoutine != null)
            StopCoroutine(deactivePlayerRoutine);
    }
}
