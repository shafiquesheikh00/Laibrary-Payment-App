namespace Laibrary_Payment_App.Models
{
    public static class CurrentSession
    {
        public static UserMaster? CurrentUser { get; set; }
        public static bool IsLoggedIn => CurrentUser != null;
        public static bool RequestLogout { get; set; }

        public static void Clear()
        {
            CurrentUser = null;
            RequestLogout = false;
        }
    }

    public static class UserSession
    {
        public static string? CurrentUserName => CurrentSession.CurrentUser?.FullName ?? CurrentSession.CurrentUser?.Username ?? "Admin";
        public static bool IsAuthenticated => CurrentSession.IsLoggedIn;
    }
}
