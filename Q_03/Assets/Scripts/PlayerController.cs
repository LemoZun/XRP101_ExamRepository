using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 100)]
    public int Hp { get; private set; }
    private bool _isDeadAudioPlayed; // ���� ���带 ������״��� ������ bool���� �߰�
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
        //while(_audio.isPlaying) //isPlaying�� ����� ���Ҹ��� ������ ���� �ѹ� �� �׾ ��� ���Ҹ��� �̾����� �÷��̾ ���� ����
        //    yield return null;
        yield return new WaitForSeconds(_audio.clip.length); // �̷��� �ϴ� �ѹ� ���Ҹ��� ������ �ٷ� ���� 

        gameObject.SetActive(false);
            
    }

    private void OnDisable()
    {
        if(deactivePlayerRoutine != null)
            StopCoroutine(deactivePlayerRoutine);
    }
}
