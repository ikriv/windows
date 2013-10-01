using System;
using System.Windows;

namespace IKriv.Windows.Mvp
{
    /// <summary>
    /// Ensures connection between a presenter and a view
    /// </summary>
    /// <typeparam name="IView">Interface implemented by the view</typeparam>
    public static class PresenterBinding<IView>
    {
        /// <summary>
        /// Establishes a connection between a view and a presenter
        /// </summary>
        /// <typeparam name="TView">Concrete type of the view</typeparam>
        /// <param name="view">Instance of the view</param>
        /// <remarks>The view must implement IView interface that allows presenters to control the view
        /// in a UI-agnostic manner. In other words, IView interface must contain as little WPF artefacts
        /// as possible.<br />
        /// BindPresenter() checks the <see cref="System.Windows.FrameworkElement.DataContext"/>DataContext</see> 
        /// of the view. If it is a presenter that implements <see cref="IPresenter">IPresenter&lt;IView&gt;</see> interface, 
        /// then DataContext.View will be set to point to the view. This allows the presenter to control the view
        /// via IView interface. The presenter therefore does not have to know about concrete type TView.
        /// </remarks>
        public static void BindPresenter<TView>(TView view) where TView : FrameworkElement, IView
        {
            Action assignView = delegate
                                    {
                                        var presenter = view.DataContext as IPresenter<IView>;
                                        if (presenter == null) return;
                                        presenter.View = view;
                                    };
            assignView();
            view.DataContextChanged += (sedner, args) => assignView();
        }
    }
}
