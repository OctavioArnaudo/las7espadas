public class JumpStatic : JumpLayer
{
    protected override void Start()
    {
        base.Start();
        if (displacementInput.y != 0)
        {
            rb.gravityScale = 0f;
        }
    }
}