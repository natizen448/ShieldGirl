using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private Vector2 boxCastSize;
    [SerializeField] private float m_boxCastMaxDistance;
    public float m_HoldDelay;
    public bool m_isAreaHoldAble;
    public ObjectInfo ObjectInfo;
    private IEnumerator areaHoldTime;

    public bool IsOnObject()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.up, m_boxCastMaxDistance, LayerMask.GetMask("Object"));
        if (raycastHit.collider != null)
            ObjectInfo = WaveController.Instance.obj_objectBox.transform.GetChild(0).GetComponent<ObjectInfo>();


        return (raycastHit.collider != null);
    }

    public void AddDelay(float holdTime)
    {
        m_isAreaHoldAble = true;
        StartCoroutine(AreaHoldTime(holdTime));
    }

    public void ChangeArea(float x, float y, float distance)
    {
        boxCastSize = new Vector2(x, y);
        m_boxCastMaxDistance = distance;
    }


    #region ±âÁî¸ð
    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up * m_boxCastMaxDistance);
        Gizmos.DrawWireCube(transform.position + (transform.up * m_boxCastMaxDistance * 0.5f), boxCastSize);

        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, transform.up, m_boxCastMaxDistance, LayerMask.GetMask("Object"));
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position, transform.up * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + (transform.up * raycastHit.distance * 0.5f), boxCastSize);
        }



    }
    #endregion

    public IEnumerator AreaHoldTime(float holdTime)
    {
        yield return YieldCache.WaitForSeconds(holdTime);
        m_isAreaHoldAble = false;
        PlayerManager.Instance.m_currentAttackDamage = PlayerManager.Instance.m_attackdamage;
    }
}
