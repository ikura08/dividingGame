using UnityEngine;

public class PlayerCoreInteraction : MonoBehaviour
{
    public AbilityManager abilityManagerScr;
    PlayerJump jump;

    void Start()
    {
        jump = GetComponent<PlayerJump>();
        abilityManagerScr = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
            abilityManagerScr.CoreChanging(collision);

            // 上から乗った場合のみジャンプ回復
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    jump.jumpCount = 1;
                    jump.isGrounded = true;
                    return;
                }
            }
        }
    }
}
