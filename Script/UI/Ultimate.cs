using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ultimate : MonoBehaviour
{
    [SerializeField] private Image ultImage;
    [SerializeField] private GameObject ultButton;

    public Transform targetObject; // 특정 오브젝트
    private Vector2 swipeStartPosition;

    public float minSwipeDist = 50f;

    public bool m_isCanUseUlt = false;

    public bool m_isRight;
    public bool m_isLeft;

    public void AddUltGauge(float percent)
    {
        ultImage.fillAmount += percent;

        if(ultImage.fillAmount >= 1)
        {
            m_isCanUseUlt = true;
            ultImage.gameObject.SetActive(false);
            ultButton.SetActive(true);
        }
    }

    public void ResetGauge()
    {
        m_isCanUseUlt = false;
        ultImage.fillAmount = 0;
        ultImage.gameObject.SetActive(true);
        ultButton.SetActive(false);
    }

    public bool TouchCheck()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // 터치한 위치가 특정 오브젝트 위에 있는지 체크
                if (IsPointerOverObject(touch.position))
                {
                    swipeStartPosition = touch.position;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeEndPosition = touch.position;
                bool isSwipingUpFromLeft = CheckSwipeUpFromLeft(swipeEndPosition);
                bool isSwipingUpFromRight = CheckSwipeUpFromRight(swipeEndPosition);

                if (m_isRight)
                    return isSwipingUpFromRight;
                else if (m_isLeft)
                    return isSwipingUpFromLeft;


            }

            
        }
       return false;
        
    }

    private bool IsPointerOverObject(Vector2 pointerPosition)
    {
        // 터치한 위치를 화면 좌표로 변환
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, Camera.main.nearClipPlane));

        // 레이캐스트를 통해 특정 오브젝트 위에 있는지 확인
        RaycastHit2D hit = Physics2D.Raycast(screenPoint, Vector2.zero);

        // 특정 오브젝트 위에 터치한 경우 true 반환
        return hit.collider != null && hit.collider.transform == targetObject;
    }

    private bool CheckSwipeUpFromLeft(Vector2 swipeEndPosition)
    {
        // 왼쪽에서 위로 스와이프하는 경우 체크
        float swipeDistanceX = swipeEndPosition.x - swipeStartPosition.x;
        float swipeDistanceY = swipeEndPosition.y - swipeStartPosition.y;

        return swipeDistanceX < 0 && swipeDistanceY > minSwipeDist;
    }

    private bool CheckSwipeUpFromRight(Vector2 swipeEndPosition)
    {
        // 오른쪽에서 위로 스와이프하는 경우 체크
        float swipeDistanceX = swipeEndPosition.x - swipeStartPosition.x;
        float swipeDistanceY = swipeEndPosition.y - swipeStartPosition.y;
            
        return swipeDistanceX > 0 && swipeDistanceY > minSwipeDist;
    }
}

