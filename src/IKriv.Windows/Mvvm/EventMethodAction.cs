using System;
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

namespace IKriv.Windows.Mvvm
{
    public class EventMethodAction : TriggerAction<DependencyObject>
    {
        public object TargetObject
        {
            get { return (object)GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetObjectProperty =
            DependencyProperty.Register("TargetObject", typeof(object), typeof(EventMethodAction), new UIPropertyMetadata(null));


        public string MethodName { get; set; }

        
        protected override void Invoke(object parameter)
        {
            if (TargetObject == null) throw new InvalidOperationException("TargetObject is null");
            if (MethodName == null) throw new InvalidOperationException("MethodName is null");

            var type = TargetObject.GetType();
            var method = type.GetMethod(MethodName,
                                        BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public |
                                        BindingFlags.NonPublic);

            if (method == null)
            {
                throw new InvalidOperationException(
                    String.Format("Method {0} not found in type {1}", MethodName, type.FullName));
            }

            var parameters = method.GetParameters();
            if (parameters.Length > 1)
            {
                throw new InvalidOperationException(String.Format("Method {0}.{1} should have zero or one argument",
                                                                  type.FullName, MethodName));
            }

            var paramValues = parameters.Length == 0
                                  ? new object[0]
                                  : new[] {parameter};

            method.Invoke(TargetObject, paramValues);
        }
    }
}
