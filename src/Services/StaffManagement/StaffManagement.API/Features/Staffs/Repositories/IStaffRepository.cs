namespace StaffManagement.API.Features.Staffs.Repositories
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetStaffs(CancellationToken cancellationToken);
        Task<Staff> GetStaffById(Guid StaffId, CancellationToken cancellationToken);
        Task<Guid> CreateStaff(Staff staff, CancellationToken cancellationToken);
        Task<bool> UpdateStaff(Staff staff, CancellationToken cancellationToken);
        Task<bool> DeleteStaff(Guid StaffId, CancellationToken cancellationToken);
    }
}
