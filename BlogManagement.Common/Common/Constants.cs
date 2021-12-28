namespace BlogManagement.Common.Common
{
    /// <summary>
    /// This class stores constants variables for using in the application.
    /// </summary>
    public static class Constants
    {
        #region Error messages

        public const string ErrorMessageLogging = "Something went wrong in the ";
        public const string ObjectAlreadyExists = "The object is already exists. Please try again. ";
        public const string InvalidArgument = "There is no object found with the provided ID.";
        public const string NoRolesFound = "There are no roles matching the provided role names. ";
        public const string FileNotFound = "The image provided doesn't exists. ";
        public const string InvalidDirectory = "The directory is invalid. ";

        #endregion

        #region Other

        public const string ParentPost = "ParentPost";
        public const string ChildPosts = "ChildPosts";
        public const string PostRatings = "PostRatings";
        public const string DefaultImage = "images/default-user.png";

        #endregion
    }
}
