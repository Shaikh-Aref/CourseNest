namespace CourseNest.Repositories
{
    public interface IEnrollmentCartRepository
    {
        public Task<int> AddItem(int courseId);
        Task<int> RemoveItem(int courseId);
        Task<EnrollmentCart> GetUserEnrollmentCart();
        Task<int> GetEnrollmentCartItemCount(string userId = "");
        Task<EnrollmentCart> GetEnrollmentCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
