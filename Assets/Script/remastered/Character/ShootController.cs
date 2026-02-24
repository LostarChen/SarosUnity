using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Bullet _Bullet;
    [SerializeField] private float _BulletSpeed = 1.0f;
    [SerializeField] private Transform _BulletSpawnPoint;
    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private List<AudioClip> _SoundEffect;
   public void ShootBullet(Player player,Transform rt)
    {
        var bullet = Instantiate(_Bullet, _BulletSpawnPoint.position, _BulletSpawnPoint.rotation);
        float direction = Mathf.Sign(rt.localScale.x);
        bullet._Rb.velocity = _BulletSpawnPoint.right * _BulletSpeed * direction;
        bullet._Renderer.color = player._Color;
        _AudioSource.PlayOneShot(_SoundEffect[Random.Range(0, 4)]);
    }
}
