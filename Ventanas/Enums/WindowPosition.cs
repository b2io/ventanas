using Base2io.Util.EnumUtil;

namespace Base2io.Ventanas.Enums
{
    public enum WindowPosition
    {
        [StringValue("Left 1/3")]
        LeftOneThird,

        [StringValue("Left 1/2")]
        LeftHalf,

        [StringValue("Left 2/3")]
        LeftTwoThirds,

        [StringValue("Right 1/3")]
        RightOneThird,

        [StringValue("Right 1/2")]
        RightHalf,

        [StringValue("Right 2/3")]
        RightTwoThirds,

        [StringValue("Top 1/2")]
        TopHalf,

        [StringValue("Bottom 1/2")]
        BottomHalf,

        [StringValue("Center")]
        Center,

        [StringValue("Fill")]
        Fill
    }
}
