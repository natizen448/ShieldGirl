using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_alphaSpeed;

    TMP_Text tmp_text;
    Color alpha;
    Color defaultAlpha;


    void Awake()
    {
        tmp_text = GetComponent<TMP_Text>();
        defaultAlpha = tmp_text.color;
        alpha = tmp_text.color;
    }

    
    void Update()
    {
        transform.Translate(new Vector2(0, m_moveSpeed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, m_alphaSpeed * Time.deltaTime);
    }

    public void ShowDamage(int _damage)
    {
        transform.position = DamageTextPool.Instance.damageTextSpawnPos.position;
        //tmp_text.color = _damage > 1000 ? Color.red : Color.white;
        tmp_text.text = _damage.ToString();
        Invoke("DestroyText", 0.7f);
    }

    public void DestroyText()
    {
        alpha = defaultAlpha;
        DamageTextPool.Instance.ReturnObject(this);
    }
}
