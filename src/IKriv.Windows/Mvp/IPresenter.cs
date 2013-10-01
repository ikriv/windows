namespace IKriv.Windows.Mvp
{
    /// <summary>
    /// Interface implemented by presenter class
    /// </summary>
    /// <typeparam name="TView">Interface of the view</typeparam>
    /// <remarks>Ther view implements the TView interface.
    /// The presenter implementes IPresenter&lt;TView&gt; interface.
    /// Use <see cref="PresenterBinding">PresenterBinding</see> class to link presenter and view instances.</remarks>
    public interface IPresenter<TView>
    {
        /// <summary>
        /// Reference to the view controlled by this presenter
        /// </summary>
        /// <remarks>Typically View property of a persenter is set by <see cref="PresenterBinding">PresenterBinding</see> class</remarks>
        TView View { get; set; }
    }
}
