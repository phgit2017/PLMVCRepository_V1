using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PL.Business.Dto.IOBalance;
namespace PL.Business.Interface.IOBalance
{
    public interface IBranchService
    {
        IQueryable<BranchDto> GetAll();
        BranchDto FindBranchById(int branchId);
        bool SaveBranch(BranchDto branchDetails);
        bool UpdateBranch(BranchDto newBranchDetails);
    }
}
