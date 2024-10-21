
namespace StaffManagement.API.Features.Staffs.Repositories
{
    public class StaffRepository(ApplicationDbContext context)
        : IStaffRepository
    {
        public async Task<Guid> CreateStaff(Staff staff, CancellationToken cancellationToken)
        {
            context.Staffs.Add(staff);
            await context.SaveChangesAsync(cancellationToken);
            return staff.StaffId;
        }

        public async Task<bool> DeleteStaff(Guid StaffId, CancellationToken cancellationToken)
        {
            var staff = await context.Staffs.SingleOrDefaultAsync(s => s.StaffId == StaffId, cancellationToken);
            if (staff == null)
            {
                throw new StaffNotFoundException(StaffId);
            }
            context.Staffs.Remove(staff);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Staff> GetStaffById(Guid StaffId, CancellationToken cancellationToken)
        {
            var staff = await context.Staffs.SingleOrDefaultAsync(s => s.StaffId == StaffId, cancellationToken);
            if (staff == null)
            {
                throw new StaffNotFoundException(StaffId);
            }

            return staff;
        }

        public async Task<IEnumerable<Staff>> GetStaffs(CancellationToken cancellationToken)
        {
            var guests = await context.Staffs.ToListAsync(cancellationToken);
            return guests;
        }

        public async Task<bool> UpdateStaff(Staff staff, CancellationToken cancellationToken)
        {
            context.Staffs.Update(staff);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
