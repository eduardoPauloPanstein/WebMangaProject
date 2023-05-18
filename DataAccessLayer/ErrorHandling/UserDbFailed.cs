using Shared;
using Shared.Responses;

namespace DataAccessLayer.ErrorHandling
{
    public static class UserDbFailed
    {
        public static Response Handle(Exception error)
        {
            if (error.Message.Contains("UQ_EMAIL"))
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse("Error when registering the user, because the email is already in use.");
            }
            else if (error.Message.Contains("UQ_NICKNAME"))
            {
                return ResponseFactory.CreateInstance().CreateFailedResponse("Error when registering the user, because the nickname is already in use.");
            }
            return ResponseFactory.CreateInstance().CreateFailedResponse("Error when registering the client, contact the admin.", error);
        }
    }
}
