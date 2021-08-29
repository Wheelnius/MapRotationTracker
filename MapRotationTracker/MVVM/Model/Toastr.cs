
namespace MapRotationTracker.MVVM.Model
{
    public class Toastr
    {
        public string Message { get; set; }
        public bool Visible { get; set; }

        public static Toastr GetHiddenToastr()
        {
            return new Toastr()
            {
                Visible = false
            };
        }
    }
}
