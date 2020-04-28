using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;
using Splat;

namespace Pradana.ReactiveUI.XamForms.Infrastructure
{
    public abstract class BaseViewModel : ReactiveObject, INavigable, IEnableLogger
    {
        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ReactiveCommand<Unit, Unit> GoBackToRoot { get; }
        public ReactiveCommand<Unit, Unit> GoBackModal { get; }

        protected IParameterViewStackService ParameterViewStackService { get; }

        public string Id { get; }

        protected CompositeDisposable Disposable { get; }

        protected BaseViewModel(IParameterViewStackService parameterViewStackService)
        {
            var type = GetType();
            this.Log().Write("View Model Initialization", type, LogLevel.Info);
            Disposable = new CompositeDisposable();
            Id = type.Name;
            ParameterViewStackService = parameterViewStackService;
            GoBack = ReactiveCommand.CreateFromObservable(() => ParameterViewStackService.PopPage());
            GoBackModal = ReactiveCommand.CreateFromObservable(() => ParameterViewStackService.PopModal());
            GoBackToRoot = ReactiveCommand.CreateFromObservable(() => ParameterViewStackService.PopToRootPage());

            GoBack
                .ThrownExceptions
                .Subscribe(x => this.Log().Write(x, nameof(GoBack), LogLevel.Error));
            GoBackModal
              .ThrownExceptions
              .Subscribe(x => this.Log().Write(x, nameof(GoBackModal), LogLevel.Error));
            GoBackToRoot
              .ThrownExceptions
              .Subscribe(x => this.Log().Write(x, nameof(GoBackToRoot), LogLevel.Error));
        }

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter)
        {
            Disposable.Dispose();
            return Observable.Return(Unit.Default);
        }

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);
    }
}
