using System;
using System.Reactive;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Pradana.ReactiveUI.Commons;
using Pradana.ReactiveUI.XamForms.Extensions;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace Pradana.ReactiveUI.XamForms.Infrastructure
{
    public abstract class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public BaseContentPage()
        {
            if (!IsRegisterInteractions)
            {
                return;
            }
            this.WhenActivated(disposable =>
            {
                Interactions
                .LoadingStarted
                .RegisterHandler(async (arg) =>
                {
                    await HandleLoadingStarted();
                    arg.SetOutput(Unit.Default);
                }).DisposeWith(disposable);

                Interactions
               .LoadingFinished
               .RegisterHandler(async (arg) =>
               {
                   await HandleLoadingFinished();
                   arg.SetOutput(Unit.Default);
               }).DisposeWith(disposable);

                Interactions
                .LoadingError
                .RegisterHandler(async (arg) =>
                {
                    await HandleLoadingError(arg.Input);
                    arg.SetOutput(Unit.Default);
                }).DisposeWith(disposable);
            });
        }

        protected virtual bool IsRegisterInteractions { get; } = false;

        protected virtual Task HandleLoadingStarted()
        {
            UserDialogs.Instance.ShowLoading();
            return Task.CompletedTask;
        }

        protected virtual Task HandleLoadingFinished()
        {
            UserDialogs.Instance.HideLoading();
            return Task.CompletedTask;
        }

        protected virtual async Task HandleLoadingError(Exception exception)
        {
            UserDialogs.Instance.HideLoading();
            await this.DisplayError(exception);
        }
    }
}
