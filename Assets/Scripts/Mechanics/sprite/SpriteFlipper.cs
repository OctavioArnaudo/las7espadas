public class SpriteFlipper : SpriteFacing
{
    public SpriteModel displacementOrientation = SpriteModel.Right;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // If the horizontal input is greater than 0.01, set the orientation to Right.
        if (displacementInput.x > 0.01f)
        {
            flipX((displacementOrientation == SpriteModel.Left));
        }
        // Check if the horizontal input is within a small range around zero to determine if the orientation should be Centered.
        else if (displacementInput.x < 0.01f && displacementInput.x > -0.01f)
        {
            flipX((displacementOrientation == SpriteModel.Centered));
        }
        // If the horizontal input is less than -0.01, set the orientation to Left.
        else if (displacementInput.x < -0.01f)
        {
            flipX((displacementOrientation == SpriteModel.Right));
        }
    }
}