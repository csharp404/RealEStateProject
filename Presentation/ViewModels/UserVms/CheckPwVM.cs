namespace Presentation.ViewModels.UserVms
{
    public class CheckPwVM
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string? ValidationMessage { get; set; }
    }
}
