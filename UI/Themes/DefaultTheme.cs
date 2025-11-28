using System;
using MudBlazor;

namespace UI.Themes;

public class DefaultTheme
{
    public static MudTheme Theme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#083d82",

            Tertiary = "#0a489aff",
            Secondary = "#edf2f5",

            Background = "#f7f8fa",
            Surface = "#FFFFFF",

            AppbarBackground = "#ffffff",
            TextPrimary = "#083d82",
            ActionDefault = "#0a489aff",
            TableHover = "#d4e7ffff",

            HoverOpacity = 0.1,
        },

        PaletteDark = new PaletteDark()
        {
            Primary = Colors.Blue.Darken4,
            Secondary = Colors.Green.Accent4,

            AppbarBackground = Colors.BlueGray.Darken3,
        },

        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Lora", "serif" },
                FontSize = "0.875rem",
                FontWeight = "700",
            },

            H6 = new H6Typography
            {
                FontWeight = "700",

                FontSize = "1rem",
            },

            Body1 = new Body1Typography { FontWeight = "500" },
            Body2 = new Body2Typography { FontWeight = "500" },
        },
    };
}
