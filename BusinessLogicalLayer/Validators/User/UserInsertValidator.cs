namespace BusinessLogicalLayer.Validators.User
{
    internal class UserInsertValidator : UserValidator
    {
        public UserInsertValidator()
        {
            base.ValidateNickname();
            base.ValidateEmail();
            base.ValidatePassword();
            //base.ValidateConfirmPassword();
        }
    }

    internal class UserUpdateValidator : UserValidator
    {
        public UserUpdateValidator()
        {
            base.ValidateNickname();
            base.ValidateId();
            //base.ValidateEmail();
            //base.ValidatePassword();
            //validate about?
        }
    }
}
