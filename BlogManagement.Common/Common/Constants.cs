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
        public const string Success = "Success!";
        public const string SuccessMessage = "Process completed successfully!";
        public const string RatingSuccessMessage = "Thank you for your rating!";
        public const string Error = "Error!";
        public const string ErrorMessage = "Something went wrong. Please try again!";
        public const string PleaseFillIn = "Please fill in the field.";
        public const string NotFoundResponse = "Can't find the object with the provided argument.";
        public const string ErrorForUser = "Something went wrong.";
        public const string ItsEmpty = "There is no data at the moment";

        #endregion

        #region Other

        public const string ParentPost = "ParentPost";
        public const string ChildPosts = "ChildPosts";
        public const string PostRatings = "PostRatings";
        public const string DefaultImage = "images/default-user.png";
        public const string HttpClientName = "blogManagement";

        #endregion
    }
}
