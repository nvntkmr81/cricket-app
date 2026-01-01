namespace Contracts.Request
{
    public class SetPasswordRequest
    {
        public required string UserName { get; set; }
        public required string NewPassword { get; set; }
    }
}
