using System;
using jQueryApi;

namespace SmartTrack.Scripts
{
    public static class Watermark
    {
        public static void spanBlur(jQueryObject span)
        {
            if (span.Siblings("input").GetValue().Trim().Length == 0)
                span.Show().CSS("z-index", "100");
        }

        public static void spanFocus(jQueryObject span)
        {
            span.Hide();
            span.Siblings("input").Focus();
        }

        public static void inputBlur(jQueryObject input)
        {
            if (input.GetValue().Trim().Length == 0)
                input.Siblings("span").Show().CSS("z-index", "100");
        }

        public static void inputFocus(jQueryObject input)
        {
            input.Siblings("span").Hide();
        }
    }
}
