using System.Collections;
using UnityEngine;

public class JumpInZeroG
{

    private Vector2 displacementInput;
    private Rigidbody2D rb;

    public JumpInZeroG(
        Rigidbody2D rb,
        Vector2 displacementInput
        )
    {
        this.rb = rb;
        this.displacementInput = displacementInput;
    }

    public virtual IEnumerator OnStart()
    {
        if (displacementInput.y != 0)
        {
            rb.gravityScale = 0f;
        }
        yield return null;
    }
}