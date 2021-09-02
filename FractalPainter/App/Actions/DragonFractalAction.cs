using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly DragonSettings dragonSettings;
        private readonly Palette palette;
        private readonly Func<IImageHolder, Palette, DragonSettings, DragonPainter> dragonFactory;

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public DragonFractalAction(
            IImageHolder imageHolder,
            DragonSettings dragonSettings,
            Palette palette,
            Func<IImageHolder,
                Palette,
                DragonSettings,
                DragonPainter> dragonFactory)
        {
            this.imageHolder = imageHolder;
            this.dragonSettings = dragonSettings;
            this.palette = palette;
            this.dragonFactory = dragonFactory;
        }

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            var painter = dragonFactory(imageHolder, palette, dragonSettings);
            painter.Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}