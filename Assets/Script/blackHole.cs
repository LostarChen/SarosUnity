using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    
    public List<Vector3> _OtherBlackHolePos = new List<Vector3>();
    [SerializeField] private int _CooldownTime = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Character character))
        {
            if (!character._IsTeleportLocked)
            {
                Vector3 pos = _OtherBlackHolePos[Random.Range(0, _OtherBlackHolePos.Count)];
                character._Rt.localPosition = pos;
                StartCoroutine(CooldownRoutine(character));
            }
        }
    }
    private IEnumerator CooldownRoutine(Character character)
    {
        character._IsTeleportLocked = true;

        yield return new WaitForSeconds(_CooldownTime);

        character._IsTeleportLocked = false;
    }
}
