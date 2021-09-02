using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App
{
    internal static class Program
    {
        private static void RunApp(IKernel container)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static void ConfigureInjections(IKernel container)
        {
            container.Bind<IUiAction>().To<SaveImageAction>();
            container.Bind<IUiAction>().To<DragonFractalAction>();
            container.Bind<IUiAction>().To<KochFractalAction>();
            container.Bind<IUiAction>().To<ImageSettingsAction>();
            container.Bind<IUiAction>().To<PaletteSettingsAction>();
        }
        
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            IKernel container = new StandardKernel();
            ConfigureInjections(container);

            RunApp(container);
        }

    }
}